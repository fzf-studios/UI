#if USE_UNIRX && USE_ZENJECT
using FZFUI.Factories;
using FZFUI.Markers;
using UniRx.Toolkit;
using UnityEngine;

namespace FZFUI.Pool
{
    public class GenericPool<T>: ObjectPool<T> where T: MonoBehaviour
    {
        private readonly T _prefab;
        private readonly PoolContainerMarker _poolContainerMarker;
        private readonly IInjectViewFactory _injectViewFactory;

        public GenericPool(T prefab, PoolContainerMarker poolContainerMarker, IInjectViewFactory injectViewFactory)
        {
            _prefab = prefab;
            _poolContainerMarker = poolContainerMarker;
            _injectViewFactory = injectViewFactory;
        }

        protected override T CreateInstance()
        {
            var instance = _injectViewFactory.Create(_prefab, _poolContainerMarker.transform);
            return instance;
        }
    }
}
#endif