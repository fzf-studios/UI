using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FZFUI.Buttons;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace FZFUI.Autogen
{
    public interface IView
    {
        public GameObject gameObject { get; }
    }

    public class BaseAutogen : MonoBehaviour, IView
    {
#if UNITY_EDITOR
        [Button("Generate", EButtonEnableMode.Editor)]
        private void GenerateFields()
        {
            var settingsAsset = GetAutogenSettings();
            var concreteClass = GetConcreteClass();
            var namespaceName = concreteClass.GetType().Namespace;
            var components = new List<Component>();
            var innerComponents = concreteClass.GetComponents<Component>().Except(new[] { concreteClass });
            var usingNames = new List<string>();
            RecursivelyGetComponents(transform, components);

            var fields = new List<string>();
            CollectComponents(components, fields, usingNames, (component) => component.gameObject.name);
            CollectComponents(innerComponents, fields, usingNames, (component) => component.GetType().Name);

            usingNames = usingNames.Distinct().ToList();
            var fileName = $"{concreteClass.GetType().Name}Autogen";
            var code = $@"using UnityEngine;
{string.Join("\n", usingNames)}

namespace {namespaceName}
{{
    public partial class {concreteClass.GetType().Name}
    {{
        {string.Join("\n\t\t", fields)}
    }}
}}
";
            var path = $"{Application.dataPath}/{settingsAsset.AutogenScriptsFolderPath}/{fileName}.cs";
            EditorApplication.LockReloadAssemblies();
            File.WriteAllText(path, code);
            AssetDatabase.Refresh();
            EditorApplication.UnlockReloadAssemblies();
        }

        private static AutogenSettings GetAutogenSettings()
        {
            var settingsAsset = Resources.Load<AutogenSettings>("AutogenSettings");
            if (settingsAsset == null)
            {
                settingsAsset = ScriptableObject.CreateInstance<AutogenSettings>();
                string defaultPath = "Assets/Resources/AutogenSettings.asset";
                AssetDatabase.CreateAsset(settingsAsset, defaultPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.LogWarning($"There was no autogen settings. Created at path {defaultPath}");
            }

            return settingsAsset;
        }

        private static void CollectComponents(IEnumerable<Component> components, List<string> fields,
            List<string> usingNames, System.Func<Component, string> namingPattern)
        {
            foreach (var component in components)
            {
                var type = component.GetType();
                var usingName = !string.IsNullOrWhiteSpace(type.Namespace)
                    ? $"using {type.Namespace};"
                    : "";
                var fieldName = $"[SerializeField] private {type.Name} {namingPattern?.Invoke(component)};";
                fields.Add(fieldName);
                if (!usingNames.Contains(usingName))
                    usingNames.Add(usingName);
            }
        }

        private static void RecursivelyGetComponents(Transform parent, List<Component> components)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                bool isAutomaticField = false;
                var component = child
                    .GetComponents<Component>()
                    .FirstOrDefault(x => x.GetType()
                        .IsSubclassOf(typeof(BaseAutogen)));

                component ??= child.GetComponents<Component>()
                    .FirstOrDefault(x => x is ISubscribableButton);

                isAutomaticField = component != null;

                component ??= child
                    .GetComponents<Component>()
                    .FirstOrDefault(x => x != x.transform
                                         && x.GetType() != typeof(CanvasRenderer));

                component ??= child.transform;
                components.Add(component);
                if (isAutomaticField)
                    continue;
                if (child.childCount > 0)
                    RecursivelyGetComponents(child, components);
            }
        }

        [Button("Assign", EButtonEnableMode.Editor)]
        private void AssignFields()
        {
            var concreteClass = GetConcreteClass();
            var components =
                new List<Component>(concreteClass.GetComponents<Component>().Except(new[] { concreteClass }));
            RecursivelyGetComponents(transform, components);

            foreach (var component in components)
            {
                var field = concreteClass.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .FirstOrDefault(x =>
                        (x.Name == component.gameObject.name || x.Name == component.GetType().Name)
                        && x.FieldType.Name == component.GetType().Name);
                if (field == null)
                    continue;

                field.SetValue(concreteClass, component);
            }

            Debug.Log("Fields assigned");
        }

        private Component GetConcreteClass()
        {
            return gameObject.GetComponents<Component>()
                .First(x => x.GetType().IsSubclassOf(typeof(BaseAutogen)));
        }

        [MenuItem("GameObject/Create New UI", false, 10)]
        private static void CreateScript(MenuCommand menuCommand)
        {
            var settingsAsset = GetAutogenSettings();
            string scriptName = Selection.activeObject.name;
            var filePath = $"{Application.dataPath}/{settingsAsset.UIScriptsFolderPath}/{scriptName}.cs";
            if (AssetDatabase.LoadAssetAtPath<MonoScript>(filePath) != null)
            {
                Debug.LogWarning($"Attempt to rewrite already existing class file!");
                return;
            }

            File.WriteAllText(filePath, $@"using FZFUI.UI.Autogen;

namespace UI
{{
    public interface I{scriptName}
    {{
    
    }}
    public partial class {scriptName}: BaseAutogen, I{scriptName}
    {{

    }}
}}");
            AssetDatabase.Refresh();
            var asset = AssetDatabase.LoadAssetAtPath(filePath, typeof(MonoScript));
            if (asset == null)
            {
                return;
            }

            var activeGameObject = Selection.activeGameObject;

            if (activeGameObject != null)
            {
                var script = asset as MonoScript;
                if (script == null)
                    return;

                activeGameObject.AddComponent(script.GetClass());
            }
        }
#endif
    }
}