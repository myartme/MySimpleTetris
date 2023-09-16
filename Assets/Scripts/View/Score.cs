using Engine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace View
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Game game;
        private int _score;
        private TextMeshProUGUI _text;
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            game.OnDeleteLines(UpdateText);
            UpdateText(0);
        }

        private void UpdateText(int score)
        {
            _score += score;
            _text.text = $"Lines: {_score}";
        }
    }
}