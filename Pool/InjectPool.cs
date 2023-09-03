#if USE_UNIRX
using FZFUI.Factories;
using FZFUI.Interfaces;
using UniRx.Toolkit;
using UnityEngine;

namespace FZFUI.Pool
{
    public class InjectPool<T>: ObjectPool<T> where T: MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _poolContainer;
        private readonly IInjectViewFactory _injectViewFactory;

        public InjectPool(T prefab, Transform poolContainer, IInjectViewFactory injectViewFactory)
        {
            _prefab = prefab;
            _poolContainer = poolContainer;
            _injectViewFactory = injectViewFactory;
        }

        protected override T CreateInstance()
        {
            var instance = _injectViewFactory.Create(_prefab, _poolContainer);
            return instance;
        }
    }
}
#endif