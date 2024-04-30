using Engine;
using UnityEngine;

namespace Service.Singleton
{
    public class SingletonHelper : MonoBehaviour
    {
        public void StoreAllSaveData()
        {
            ColorTheme.Instance.StoreSaveData();
            MixerController.Instance.StoreSaveData();
            Localization.Instance.StoreSaveData();
        }
        
        public void ResetAllSaveData()
        {
            ColorTheme.Instance.ResetSaveData();
            MixerController.Instance.ResetSaveData();
            Localization.Instance.ResetSaveData();
        }
        
        #region MixerController
        public void ToggleMasterMixer(bool value)
        {
            MixerController.Instance.ToggleMasterMixer(value);
        }
        
        public void ToggleMasterMixer()
        {
            MixerController.Instance.ToggleMasterMixer();
        }
        
        public void ChangeMasterMixer(float volume)
        {
            MixerController.Instance.ChangeMasterMixer(volume);
        }

        public void ChangeMusicsVolume(float volume)
        {
            MixerController.Instance.ChangeMusicsVolume(volume);
        }

        public void ChangeEffectsVolume(float volume)
        {
            MixerController.Instance.ChangeEffectsVolume(volume);
        }

        public void ChangeButtonsVolume(float volume)
        {
            MixerController.Instance.ChangeButtonsVolume(volume);
        }
        #endregion

        #region ColorTheme
        public void ChangeColorTheme(bool theme)
        {
            ColorTheme.Instance.ChangeColorTheme(theme);
        }
        #endregion

        #region Localization
        public void ChangeLocale(float locale)
        {
            Localization.Instance.ChangeLocale(locale);
        }
        #endregion
    }
}