using UnityEngine;

namespace FZFUI.Markers
{
    public class PoolContainerMarker : MonoBehaviour
    {
        public static PoolContainerMarker Instance { get; private set; }
        private void Awake() => Instance = this;
    }
}