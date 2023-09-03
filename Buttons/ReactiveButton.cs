#if USE_UNIRX
using System;
using FZFUI.Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace FZFUI.Buttons
{
    /// <summary>
    /// Button with UniRX support.
    /// </summary>
    public class ReactiveButton : MonoBehaviour, IReactiveButton, IDisposable
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image image;

        private CompositeDisposable _commandDisposable;
        private CompositeDisposable _textDisposable;
        private CompositeDisposable _iconDisposable;

        public IDisposable SubscribeCommand(IObservable<bool> condition, Action action)
        {
            ResetCommand();

            var command = condition
                .ToReactiveCommand()
                .AddTo(_commandDisposable);

            command
                .Subscribe(_ => action?.Invoke())
                .AddTo(_commandDisposable);

            command
                .BindTo(button)
                .AddTo(_commandDisposable);

            return _commandDisposable;
        }

        public IDisposable Subscribe(Action action)
        {
            ResetCommand();
            
            button
                .OnClickAsObservable()
                .Subscribe(_ => action?.Invoke())
                .AddTo(_commandDisposable);
            
            return _commandDisposable;
        }

        public IDisposable SubscribeText(IObservable<string> textSequence)
        {
            ResetText();

            textSequence
                .Subscribe(newText => text.SetText(newText))
                .AddTo(_textDisposable);

            return _textDisposable;
        }

        public void SetText(string value)
        {
            ResetText();
            text.SetText(value);
        }

        public IDisposable SubscribeIcon(IObservable<Sprite> spriteSequence)
        {
            ResetIcon();

            spriteSequence
                .Subscribe(newIcon => image.sprite = newIcon)
                .AddTo(_iconDisposable);

            return _iconDisposable;
        }

        public void SetIcon(Sprite sprite)
        {
            ResetIcon();
            image.sprite = sprite;
        }

        private void OnDestroy()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            _commandDisposable?.Dispose();
            _commandDisposable = null;
            
            _textDisposable?.Dispose();
            _textDisposable = null;
            
            _iconDisposable?.Dispose();
            _iconDisposable = null;
        }
        
        private void ResetCommand()
        {
            _commandDisposable?.Dispose();
            _commandDisposable = new(3);
        }
        
        private void ResetText()
        {
            _textDisposable?.Dispose();
            _textDisposable = new(1);
            text.SetText("");
        }
        
        private void ResetIcon()
        {
            _iconDisposable?.Dispose();
            _iconDisposable = new(1);
            image.sprite = null;
        }
    }
}
#endif