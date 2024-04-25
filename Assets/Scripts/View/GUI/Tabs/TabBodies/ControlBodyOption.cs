using GameInput;
using UnityEngine;
using View.GUI.TextField;

namespace View.GUI.Tabs.TabBodies
{
    public class ControlBodyOption : TabBody
    {
        [SerializeField] private Mover mover;
        [SerializeField] private SliderValue horizontalMS;
        [SerializeField] private SliderValue verticalMS;
        
        protected override void UpdateValues()
        {
            horizontalMS.ChangeSliderValue(mover.HorizontalSpeed);
            verticalMS.ChangeSliderValue(mover.VerticalSpeed);
        }
    }
}