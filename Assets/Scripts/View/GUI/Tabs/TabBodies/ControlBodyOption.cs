using GameInput;
using UnityEngine;
using View.GUI.TextField;

namespace View.GUI.Tabs.TabBodies
{
    public class ControlBodyOption : TabBody
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private SliderValue _horizontalMS;
        [SerializeField] private SliderValue _verticalMS;
        
        protected override void UpdateValues()
        {
            _horizontalMS.ChangeSliderValue(_mover.HorizontalSpeed);
            _verticalMS.ChangeSliderValue(_mover.VerticalSpeed);
        }
    }
}