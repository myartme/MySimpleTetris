using Engine;
using UnityEngine;

namespace View.GUI.Tetromino
{
    public class TetrominoContainer : MonoBehaviour
    {
        private void OnEnable()
        {
            //!!!!!
            ColorTheme.OnCurrentBlock += ChangeBlocks;
        }

        private void OnDisable()
        {
            ColorTheme.OnCurrentBlock -= ChangeBlocks;
        }

        private void ChangeBlocks()
        {
            foreach (Transform tetromino in gameObject.transform)
            {
                foreach (Transform block in tetromino.transform)
                {
                    var sr = block.GetComponent<SpriteRenderer>();
                    sr.sprite = ColorTheme.Instance.CurrentBlock;
                }
            }
        }
    }
}