using System.Collections;
using Engine;
using UnityEngine;
using UnityEngine.Serialization;
using View.GUI.Buttons;
using View.GUI.TextField;

namespace View.GUI.Tabs.TabBodies
{
    public sealed class AudioBodyOption : TabBody
    {
        [FormerlySerializedAs("_mixerController")] [SerializeField] private MixerController mixerController;
        [FormerlySerializedAs("_toggleMusic")] [SerializeField] private ToggleSwitchButton toggleMusic;
        [FormerlySerializedAs("_music")] [SerializeField] private SliderValue music;
        [FormerlySerializedAs("_buttons")] [SerializeField] private SliderValue buttons;
        [FormerlySerializedAs("_effects")] [SerializeField] private SliderValue effects;
        
        protected override void UpdateValues()
        {
            StartCoroutine(UpdateElements());
        }

        private IEnumerator UpdateElements()
        {
            yield return toggleMusic.SetToggle(mixerController.IsMasterEnabled);
            yield return music.ChangeSliderValueIfIsset(mixerController.MusicValue);
            yield return effects.ChangeSliderValueIfIsset(mixerController.EffectsValue);
            yield return buttons.ChangeSliderValueIfIsset(mixerController.UIValue);
        }
    }
}