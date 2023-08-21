using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace FZFUI.Buttons
{
    public interface IText
    {
        public void SetText(string text);
        public void SubscribeText(IObservable<string> property);
    }
    
    public class TextButton: BasicButton, IText
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