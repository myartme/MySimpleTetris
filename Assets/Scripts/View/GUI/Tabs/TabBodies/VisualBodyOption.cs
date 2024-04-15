using Engine;
using UnityEngine;
using View.GUI.Buttons;

namespace View.GUI.Tabs.TabBodies
{
    public class VisualBodyOption : TabBody
    {
        [SerializeField] private ColorTheme _colorTheme;
        [SerializeField] private ToggleSwitchButton _toggleTheme;
        [SerializeField] private BlocksSelector _blocksSelector;

        protected override void UpdateValues()
        {
            //_toggleTheme.IsToggleOn = _colorTheme.CurrentThemeId == 1;
            //_blocksSelector.UpdateBlockSprite();
        }
    }
}