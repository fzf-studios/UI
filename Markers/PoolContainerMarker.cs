using UnityEngine;

namespace FZFUI.Markers
{
    public class PoolContainerMarker : MonoSingleton<PoolContainerMarker>
    {
        public static Transform Transform => Instance.transform;
    }
}