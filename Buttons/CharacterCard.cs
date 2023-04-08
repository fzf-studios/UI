using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Architecture.UI.Buttons
{
    public class CharacterCard : MonoBehaviour
    {
        [SerializeField] private TextButton MainButton;
        [SerializeField] private TextButton InfoButton;
        [SerializeField] private TextMeshProUGUI CardDescription;
        [SerializeField] private RectTransform CharacterImage;

        private bool _infoShown;
        private bool _isAnimating;

        private void Start()
        {
            InfoButton.Subscribe(() =>
            {
                if (_isAnimating)
                    return;

                var sequence = DOTween.Sequence();
                sequence.OnComplete(() => _isAnimating = false);
                _infoShown = !_infoShown;
                _isAnimating = true;
                if (_infoShown)
                {
                    sequence.Append(CharacterImage.DOScaleY(0, 0.5f));
                    sequence.Append(CardDescription.rectTransform.DOScaleY(1, 0.5f));
                    sequence.Play();
                    return;
                }

                sequence.Append(CardDescription.rectTransform.DOScaleY(0, 0.5f));
                sequence.Append(CharacterImage.DOScaleY(1, 0.5f));
                sequence.Play();
            });
        }
    }
}