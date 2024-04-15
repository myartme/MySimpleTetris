using Engine;
using UnityEngine;

namespace View.GUI.Tabs.TabBodies
{
    public class LanguageBodyOption : TabBody
    {
        [SerializeField] private Localization _localization;
        
        public void ChangeLocale(float value)
        {
            //_localization.ChangeLocale(value);
        }
    }
}