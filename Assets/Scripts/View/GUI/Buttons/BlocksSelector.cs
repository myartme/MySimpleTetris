using Engine;
using UnityEngine;
using UnityEngine.UI;

namespace View.GUI.Buttons
{
    public class BlocksSelector : MonoBehaviour
    {
        [SerializeField] private ResourcesButton _left;
        [SerializeField] private ResourcesButton _right;
        [SerializeField] private Image _blockImage;
        [SerializeField] private ColorTheme _colorTheme;
        
        private void OnEnable()
        {
            UpdateBlockSprite();
        }

        public void NextBlock()
        {
            _colorTheme.IncrementBlockId();
            UpdateBlockSprite();
        }

        public void PreviousBlock()
        {
            _colorTheme.DecrementBlockId();
            UpdateBlockSprite();
        }

        private void UpdateBlockSprite()
        {
            _blockImage.sprite = _colorTheme.CurrentBlock;
        }
    }
}