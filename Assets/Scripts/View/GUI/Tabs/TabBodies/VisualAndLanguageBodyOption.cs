using UnityEngine;

namespace View.GUI.Tabs.TabBodies
{
    public class VisualAndLanguageBodyOption : TabBody
    {
        [SerializeField] private VisualBodyOption visualBody;
        [SerializeField] private LanguageBodyOption languageBody;
        
        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            visualBody.SetActive(isActive);
            languageBody.SetActive(isActive);
        }
    }
}