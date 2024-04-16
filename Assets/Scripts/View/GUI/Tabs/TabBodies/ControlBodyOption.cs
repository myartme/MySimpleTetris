using System.Collections;
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
            /*_horizontalMS.ChangeSliderValue(_mover.HorizontalSpeed);
            _verticalMS.ChangeSliderValue(_mover.VerticalSpeed);*/
            StartCoroutine(UpdateSliders());
        }

        private IEnumerator UpdateSliders()
        {
            yield return _horizontalMS.ChangeSliderValueIfIsset(_mover.HorizontalSpeed);
            yield return _verticalMS.ChangeSliderValueIfIsset(_mover.VerticalSpeed);
        }
    }
}