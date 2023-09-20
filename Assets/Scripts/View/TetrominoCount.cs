using Engine;
using TMPro;
using UnityEngine;

namespace View
{
    public class TetrominoCount : MonoBehaviour
    {
        private int _score = -1;
        private TextMeshProUGUI _text;
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            GameGrid.OnGetTetromino += UpdateText;
        }

        private void UpdateText()
        {
            _score++;
            _text.text = $"Tetrominos: {_score}";
        }
    }
}