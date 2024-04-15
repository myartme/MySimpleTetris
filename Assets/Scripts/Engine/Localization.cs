using System.Collections;
using Save.Data.Format;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Engine
{
    public class Localization : MonoBehaviour
    {
        public OptionsSave Save
        {
            get => Saver.SaveData.options;
            private set => Saver.SaveData.options = value;
        }
        
        private readonly string _language = "Language";

        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;
            
            InitializeSaveData();
            LoadSaveData();

            yield return null;
        }

        public void StoreSaveData()
        {
            Saver.Store.Save(Saver.SaveData);
        }
        
        public void ResetSaveData()
        {
            Save = Saver.Store.Load()?.options;
            LoadSaveData();
        }

        public void ChangeLocale(float locale)
        {
            Save.SetValueByName(_language, locale);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)locale];
        }

        private void InitializeSaveData()
        {
            Save.AddToList(_language, 0f);
            StoreSaveData();
        }

        private void LoadSaveData()
        {
            ChangeLocale(Save.GetValue(_language));
        }
    }
}