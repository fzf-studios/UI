#if USE_UNIRX && USE_ZENJECT
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FZFUI.Factories;
using FZFUI.Markers;
using UnityEngine;

namespace FZFUI.Pool
{
    public interface IPoolController
    {
        void CreatePool<T>(T prefab) where T: MonoBehaviour;
        UniTask CreatePoolAsync<T>(T prefab) where T: MonoBehaviour;
        T Rent<T>() where T: MonoBehaviour;
        void Return<T>(T obj) where T: MonoBehaviour;
    }
    public class PoolController: IPoolController, IDisposable
    {
        private readonly IInjectViewFactory _injectViewFactory;
        private readonly Dictionary<Type, IDisposable> _pools = new();
        private static PoolContainerMarker PoolContainerMarker => PoolContainerMarker.Instance;

        public PoolController(IInjectViewFactory injectViewFactory)
        {
            _injectViewFactory = injectViewFactory;
        }

        public void CreatePool<T>(T prefab) where T: MonoBehaviour
        {
            var type = typeof(T);
            if (_pools.ContainsKey(type))
                return;

            _pools.Add(type, new GenericPool<T>(prefab, PoolContainerMarker, _injectViewFactory));
        }

        public UniTask CreatePoolAsync<T>(T prefab) where T : MonoBehaviour
        {
            CreatePool(prefab);
            return UniTask.CompletedTask;
        }

        public T Rent<T>() where T: MonoBehaviour
        {
            if (!_pools.ContainsKey(typeof(T)))
                return null;
            var pool = (GenericPool<T>)_pools[typeof(T)];
            
            return pool.Rent();
        }
        
        public void Return<T>(T obj) where T: MonoBehaviour
        {
            if (!_pools.ContainsKey(typeof(T)))
                return;
            var pool = (GenericPool<T>)_pools[typeof(T)];
            obj.transform.SetParent(PoolContainerMarker.transform);
            pool.Return(obj);
        }

        public void Dispose()
        {
            var disposables = _pools
                .Select(p => p.Value);
            
            foreach (var disposable in disposables) 
                disposable.Dispose();
        }
    }
}
#endif