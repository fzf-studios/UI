#if USE_UNIRX
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FZFUI.Interfaces
{
    public interface IPoolController
    {
        void CreatePool<T>(T prefab) where T: MonoBehaviour;
        UniTask CreatePoolAsync<T>(T prefab) where T: MonoBehaviour;
        T Rent<T>() where T: MonoBehaviour;
        void Return<T>(T obj) where T: MonoBehaviour;
    }
}
#endif