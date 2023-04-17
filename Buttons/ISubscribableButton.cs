using System;
using UnityEngine.Events;

namespace FZFUI.UI.Buttons
{
    public interface ISubscribableButton
    {
        public void Subscribe(UnityAction action);
        public void Unsubscribe(UnityAction action);
        public void SetInteractable(bool interactable);
        public void SubscribeInteractable(IObservable<bool> property);
        public void UnsubscribeAll();
    }
}