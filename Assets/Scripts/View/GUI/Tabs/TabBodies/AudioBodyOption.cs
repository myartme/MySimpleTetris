using Engine;
using UnityEngine;
using View.GUI.Buttons;
using View.GUI.TextField;

namespace View.GUI.Tabs.TabBodies
{
    public sealed class AudioBodyOption : TabBody
    {
        [SerializeField] private ToggleSwitchButton toggleMusic;
        [SerializeField] private SliderValue music;
        [SerializeField] private SliderValue buttons;
        [SerializeField] private SliderValue effects;

        protected override void UpdateValues()
        {
            toggleMusic.SetToggle(MixerController.Instance.IsMasterEnabled);
            music.ChangeSliderValue(MixerController.Instance.MusicValue);
            effects.ChangeSliderValue(MixerController.Instance.EffectsValue);
            buttons.ChangeSliderValue(MixerController.Instance.InterfaceValue);
        }
    }
}