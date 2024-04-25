using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.GUI.TextField
{
    public class SliderValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private bool _textToInt;
        [SerializeField][Range(1, 100)] private int _countValues = 1;
        [SerializeField][Range(0, 1)] private float _min;
        [SerializeField][Range(0, 1)] private float _max;
        private Slider _slider;
        private float _currentValue = -1;
        public event Action OnSliderValueChanged;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnValidate()
        {
            _min = RoundValue(_min);
            _max = RoundValue(_max);
            if (_min > _max)
            {
                _min = _max;
            }
        }

        private void Start()
        {
            _slider.onValueChanged.AddListener(ChangeSliderValue);
            _slider.minValue = _min;
            _slider.maxValue = _max;
        }
        
        public void ChangeSliderTValueIfIsset(float value)
        {
            ChangeSliderValue(value);
        }

        public IEnumerator ChangeSliderValueIfIsset(float value)
        {
            yield return new WaitWhile(() => _slider == null);
            ChangeSliderValue(value);
        }

        public void ChangeSliderValue(float value)
        {
            var newValue = RoundValue(value);
            if (newValue < _min)
                newValue = _min;
            if (newValue > _max)
                newValue = _max;

            _value.text = _textToInt 
                ? (newValue * _countValues).ToString() 
                : newValue.ToString(CultureInfo.InvariantCulture);
            
            if (newValue == 0)
            {
                newValue = (float)0.000001;
                _value.text = "0";
            }

            if (_currentValue.Equals(-1))
            {
                _currentValue = newValue;
            }

            _slider.value = newValue;
            
            if (_currentValue.Equals(newValue)) return;
            _currentValue = newValue;
            OnSliderValueChanged?.Invoke();
        }

        private float RoundValue(float value)
        {
            return Mathf.Round(value * _countValues) / _countValues;
        }
    }
}