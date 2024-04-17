using System.Collections;
using Engine;
using UnityEngine;
using View.GUI.Buttons;

namespace View.GUI.Tabs.TabBodies
{
    public class VisualBodyOption : TabBody
    {
        [SerializeField] private ColorTheme colorTheme;
        [SerializeField] private ToggleSwitchButton toggleTheme;
        [SerializeField] private BlocksSelector blocksSelector;

        protected override void UpdateValues()
        {
            StartCoroutine(UpdateElements());
            blocksSelector.UpdateBlockSprite();
        }
        
        private IEnumerator UpdateElements()
        {
            yield return toggleTheme.SetToggle(colorTheme.CurrentThemeId == 1);
        }
    }
}