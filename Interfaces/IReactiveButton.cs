using System;
using UnityEngine;

namespace FZFUI.Interfaces
{
    public interface IReactiveButton : IAutogenPrioritized
    {
        public IDisposable SubscribeCommand(IObservable<bool> condition, Action action);
        public IDisposable Subscribe(Action action);
        public IDisposable SubscribeText(IObservable<string> textSequence);
        public void SetText(string value);
        public IDisposable SubscribeIcon(IObservable<Sprite> spriteSequence);
        public void SetIcon(Sprite sprite);
    }
}