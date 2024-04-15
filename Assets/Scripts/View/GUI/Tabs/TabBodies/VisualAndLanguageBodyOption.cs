using Engine;
using UnityEngine;

namespace View.GUI.Tabs.TabBodies
{
    public class VisualAndLanguageBodyOption : TabBody
    {
        [SerializeField] private VisualBodyOption _visualBody;
        [SerializeField] private LanguageBodyOption _languageBody;
        
        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            //_visualBody.SetActive(isActive);
            //_languageBody.SetActive(isActive);
        }
    }
}