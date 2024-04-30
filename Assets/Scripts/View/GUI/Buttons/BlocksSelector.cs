using Engine;
using UnityEngine;
using UnityEngine.UI;

namespace View.GUI.Buttons
{
    public class BlocksSelector : MonoBehaviour
    {
        [SerializeField] private ResourcesButton left;
        [SerializeField] private ResourcesButton right;
        [SerializeField] private Image blockImage;
        
        public void UpdateBlockSprite()
        {
            blockImage.sprite = ColorTheme.Instance.CurrentBlock;
        }
        
        public void NextBlock()
        {
            ColorTheme.Instance.IncrementBlockId();
            UpdateBlockSprite();
        }

        public void PreviousBlock()
        {
            ColorTheme.Instance.DecrementBlockId();
            UpdateBlockSprite();
        }
    }
}