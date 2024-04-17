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
        [SerializeField] private ColorTheme colorTheme;
        
        public void UpdateBlockSprite()
        {
            blockImage.sprite = colorTheme.CurrentBlock;
        }
        
        public void NextBlock()
        {
            colorTheme.IncrementBlockId();
            UpdateBlockSprite();
        }

        public void PreviousBlock()
        {
            colorTheme.DecrementBlockId();
            UpdateBlockSprite();
        }
    }
}