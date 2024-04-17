using Engine;
using UnityEngine;

namespace View.GUI.Tabs.TabBodies
{
    public class LanguageBodyOption : TabBody
    {
        [SerializeField] private Localization localization;
        
        public void ChangeLocale(float value)
        {
            localization.ChangeLocale(value);
        }
    }
}