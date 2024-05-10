using System.Collections;
using Save.Data.SaveDataElements;
using Service.Singleton;
using UnityEngine.Localization.Settings;

namespace Engine
{
    public class Localization : AbstractSingleton<Localization>
    {
        private LanguageOptions Save => Saver.SaveData.Language;

        private void Awake()
        {
            GetComponent<ISingularObject>().Initialize();
        }

        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;
            LoadSaveData();
        }

        public void ChangeLocale(float locale)
        {
            Save.Language = locale;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)locale];
        }

        private void LoadSaveData()
        {
            if(!LocalizationSettings.InitializationOperation.IsDone) return;
            ChangeLocale(Save.Language);
        }
        
        private void OnEnable()
        {
            Saver.OnLoadData += LoadSaveData;
        }

        private void OnDisable()
        {
            Saver.OnLoadData -= LoadSaveData;
        }
    }
}