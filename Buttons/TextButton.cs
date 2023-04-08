using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Architecture.UI.Buttons
{
    public interface IText<T>
    {
        public void SetText(string text);
        public void SubscribeText(IObservable<string> property);
    }
    
    public class TextButton: BasicButton, IText<object>
    {
        [SerializeField] private TextMeshProUGUI Label;

        public void SetText(string text)
        {
            Label.text = text;
        }

        public void SubscribeText(IObservable<string> property)
        {
            property.Subscribe(SetText)
                .AddTo(CompositeDisposable);
        }
    }
}