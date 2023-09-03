#if USE_UNIRX
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FZFUI.Factories;
using FZFUI.Interfaces;
using FZFUI.Markers;
using UnityEngine;

namespace FZFUI.Pool
{
    public class PoolController: IPoolController, IDisposable
    {
        private readonly IInjectViewFactory _injectViewFactory;
        private readonly Dictionary<Type, IDisposable> _pools = new();
        private static Transform PoolContainer => PoolContainerMarker.Transform;

        public PoolController(IInjectViewFactory injectViewFactory)
        {
            _injectViewFactory = injectViewFactory;
        }

        public void CreatePool<T>(T prefab) where T: MonoBehaviour
        {
            var type = typeof(T);
            if (_pools.ContainsKey(type))
                return;

            _pools.Add(type, new InjectPool<T>(prefab, PoolContainer, _injectViewFactory));
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
            var pool = (InjectPool<T>)_pools[typeof(T)];
            
            return pool.Rent();
        }
        
        public void Return<T>(T obj) where T: MonoBehaviour
        {
            if (!_pools.ContainsKey(typeof(T)))
                return;
            var pool = (InjectPool<T>)_pools[typeof(T)];
            obj.transform.SetParent(PoolContainer);
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