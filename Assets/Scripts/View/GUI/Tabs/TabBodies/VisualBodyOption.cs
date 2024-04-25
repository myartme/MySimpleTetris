using System.Collections;
using Engine;
using UnityEngine;
using View.GUI.Buttons;

namespace View.GUI.Tabs.TabBodies
{
    public class VisualBodyOption : TabBody
    {
        [SerializeField] private ToggleSwitchButton toggleTheme;
        [SerializeField] private BlocksSelector blocksSelector;

        protected override void UpdateValues()
        {
            toggleTheme.SetTToggle(ColorTheme.Instance.CurrentThemeId == 1);
            //StartCoroutine(UpdateElements());
            blocksSelector.UpdateBlockSprite();
        }
        
        private IEnumerator UpdateElements()
        {
            yield return toggleTheme.SetToggle(ColorTheme.Instance.CurrentThemeId == 1);
        }
    }
}