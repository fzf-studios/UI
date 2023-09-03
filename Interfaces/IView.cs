using UnityEngine;

namespace FZFUI.Interfaces
{
    public interface IView : IAutogenPrioritized
    {
        public GameObject GameObject { get; }
        public void Show();
        public void Close();
    }
}