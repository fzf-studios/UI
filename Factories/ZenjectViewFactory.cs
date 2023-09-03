#if USE_ZENJECT
using UnityEngine;
using Zenject;

namespace FZFUI.Factories
{
    public class ZenjectViewFactory : IInjectViewFactory
    {
        private readonly DiContainer _diContainer;

        public ZenjectViewFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Create<T>(T prefab, Transform parent) where T : MonoBehaviour
        {
            return _diContainer.InstantiatePrefabForComponent<T>(prefab, parent);
        }
    }
}
#endif