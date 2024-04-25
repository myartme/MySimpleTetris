using Engine;

namespace View.GUI.Tabs.TabBodies
{
    public class LanguageBodyOption : TabBody
    {
        public void ChangeLocale(float value)
        {
            Localization.Instance.ChangeLocale(value);
        }
    }
}