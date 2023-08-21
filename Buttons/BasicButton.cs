using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FZFUI.Buttons
{
    public class BasicButton : MonoBehaviour, ISubscribableButton
    {
        [SerializeField] private Button Button;
        protected readonly CompositeDisposable CompositeDisposable = new();

        public void SetSilentInteractable(bool interactable)
        {
            Button.image.raycastTarget = interactable;
        }

        public void SetColor(Color32 color)
        {
            Button.image.color = color;
        }

        public void SetInteractable(bool interactable)
        {
            Button.interactable = interactable;
        }

        public IDisposable SubscribeInteractable(IObservable<bool> property)
        {
            return property
                .Subscribe(value => Button.interactable = value)
                .AddTo(this);
        }

        public IDisposable Subscribe(Action action)
        {
            return Button
                .OnClickAsObservable()
                .Subscribe(_ => action?.Invoke())
                .AddTo(this);
        }

        public void Unsubscribe(UnityAction action)
        {
            Button.onClick.RemoveListener(action);
        }

        public void UnsubscribeAll()
        {
            CompositeDisposable.Dispose();
            Button.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}