using UnityEngine;

namespace View.GUI.TextField
{
    [RequireComponent(typeof(AudioSource), typeof(SliderValue))]
    public class SliderSoundChangeValue : MonoBehaviour
    {
        private AudioSource _audioSource;
        private SliderValue _sliderValue;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _sliderValue = GetComponent<SliderValue>();
        }

        private void OnSliderValueChanged()
        {
            _audioSource.Play();
        }
        
        private void OnEnable()
        {
            _sliderValue.OnSliderValueChanged += OnSliderValueChanged;
        }

        private void OnDisable()
        {
            _sliderValue.OnSliderValueChanged -= OnSliderValueChanged;
        }
    }
}