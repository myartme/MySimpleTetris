using System.Collections;
using Engine;
using UnityEngine;
using View.GUI.Buttons;
using View.GUI.TextField;

namespace View.GUI.Tabs.TabBodies
{
    public sealed class AudioBodyOption : TabBody
    {
        [SerializeField] private MixerController _mixerController;
        [SerializeField] private ToggleSwitchButton _toggleMusic;
        [SerializeField] private SliderValue _music;
        [SerializeField] private SliderValue _buttons;
        [SerializeField] private SliderValue _effects;
        
        protected override void UpdateValues()
        {
            //_toggleMusic.IsToggleOn = _mixerController.IsMasterEnabled;
            //_music.ChangeSliderValue(_mixerController.MusicValue);
            //_effects.ChangeSliderValue(_mixerController.EffectsValue);
            //_buttons.ChangeSliderValue(_mixerController.UIValue);
            //StartCoroutine(UpdateSliders());
        }

        private IEnumerator UpdateSliders()
        {
            yield return _music.ChangeSliderValueIfIsset(_mixerController.MusicValue);
            yield return _effects.ChangeSliderValueIfIsset(_mixerController.EffectsValue);
            yield return _buttons.ChangeSliderValueIfIsset(_mixerController.UIValue);
        }
    }
}