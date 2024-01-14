using Engine;
using UnityEngine;
using UnityEngine.UI;

namespace View.GUI.Screen
{
    public class GameOptionsScreen : BaseScreen<GameOptionsScreen>
    {
        [SerializeField] private MixerController _mixerController;
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _buttons;
        [SerializeField] private Slider _effects;

        private void Start()
        {
            UpdateSliderValues();
        }

        public void UpdateSliderValues()
        {
            _music.value = _mixerController.MusicValue;
            _effects.value = _mixerController.EffectsValue;
            _buttons.value = _mixerController.UIValue;
        }

        public void ShowScreen(bool isShowScreen)
        {
            SetShowScreen(isShowScreen);
        }
    }
}