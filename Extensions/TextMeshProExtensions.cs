#if UNITY_2022_1_OR_NEWER
using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

namespace FZFUI.Extensions
{
    public static class TextMeshProExtensions
    {
        public static Tween AnimateTextWriting(this TextMeshProUGUI textComponent, string text, float duration)
        {
            textComponent.maxVisibleCharacters = 0;
            textComponent.text = text;
            return DOTween.To(() => textComponent.maxVisibleCharacters, 
                x => textComponent.maxVisibleCharacters = x, 
                text.Length, 
                duration);
        }
        public static Tween AnimateTextWritingPerCharacterDuration(this TextMeshProUGUI textComponent, string text, float duration)
        {
            textComponent.maxVisibleCharacters = 0;
            textComponent.text = text;
            var fullDuration = duration * text.Length;
            return DOTween.To(() => textComponent.maxVisibleCharacters, 
                x => textComponent.maxVisibleCharacters = x, 
                text.Length, 
                fullDuration);
        }
        
        public static string ToShortTimeSpan(this TimeSpan timeSpan)
        {
            var parts = new List<string>();

            if (timeSpan.Days > 0)
                parts.Add($"{timeSpan.Days} {(timeSpan.Days == 1 ? "day" : "days")}");

            if (timeSpan.Hours > 0)
                parts.Add($"{timeSpan.Hours} {(timeSpan.Hours == 1 ? "hour" : "hours")}");

            if (timeSpan.Minutes > 0)
                parts.Add($"{timeSpan.Minutes} {(timeSpan.Minutes == 1 ? "min" : "mins")}");

            if (timeSpan.Seconds > 0)
                parts.Add($"{timeSpan.Seconds} {(timeSpan.Seconds == 1 ? "sec" : "secs")}");
            
            if (timeSpan is { Milliseconds: > 0, Seconds: < 1 })
                parts.Add($"{timeSpan.Milliseconds} {(timeSpan.Milliseconds == 1 ? "ms" : "ms")}");

            return string.Join(" ", parts);
        }
    }
}
#endif