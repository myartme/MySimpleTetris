using System.Collections;
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
            toggleMusic.SetTToggle(MixerController.Instance.IsMasterEnabled);
            music.ChangeSliderTValueIfIsset(MixerController.Instance.MusicValue);
            effects.ChangeSliderTValueIfIsset(MixerController.Instance.EffectsValue);
            buttons.ChangeSliderTValueIfIsset(MixerController.Instance.UIValue);
            //StartCoroutine(UpdateElements());
        }

        private IEnumerator UpdateElements()
        {
            yield return toggleMusic.SetToggle(MixerController.Instance.IsMasterEnabled);
            yield return music.ChangeSliderValueIfIsset(MixerController.Instance.MusicValue);
            yield return effects.ChangeSliderValueIfIsset(MixerController.Instance.EffectsValue);
            yield return buttons.ChangeSliderValueIfIsset(MixerController.Instance.UIValue);
        }
    }
}