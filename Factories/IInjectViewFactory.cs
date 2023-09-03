using UnityEngine;

namespace FZFUI.Factories
{
    public interface IInjectViewFactory
    {
        T Create<T>(T prefab, Transform parent) where T : MonoBehaviour;
    }
}