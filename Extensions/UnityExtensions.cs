#if UNITY_2022_1_OR_NEWER
using DG.Tweening;
using TMPro;

namespace FZFUI.UI.Extensions
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
    }
}
#endif