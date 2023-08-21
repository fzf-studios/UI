using System;
using UnityEngine.Events;

namespace FZFUI.Buttons
{
    public interface ISubscribableButton
    {
        public IDisposable Subscribe(Action action);
        public void Unsubscribe(UnityAction action);
        public void SetInteractable(bool interactable);
        public IDisposable SubscribeInteractable(IObservable<bool> property);
        public void UnsubscribeAll();
    }
}