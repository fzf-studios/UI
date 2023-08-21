using UnityEngine;
using UnityEngine.UI;

namespace FZFUI.Buttons
{
    public interface IIcon
    {
        void SetIcon(Sprite sprite);
    }
    public class IconTextButton: TextButton, IIcon
    {
        [SerializeField] private Image Icon;
        
        public void SetIcon(Sprite sprite)
        {
            Icon.sprite = sprite;
        }
    }
}