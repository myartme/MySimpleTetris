using System.Collections;
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
            horizontalMS.ChangeSliderTValueIfIsset(mover.HorizontalSpeed);
            verticalMS.ChangeSliderTValueIfIsset(mover.VerticalSpeed);
            //StartCoroutine(UpdateSliders());
        }

        private IEnumerator UpdateSliders()
        {
            yield return horizontalMS.ChangeSliderValueIfIsset(mover.HorizontalSpeed);
            yield return verticalMS.ChangeSliderValueIfIsset(mover.VerticalSpeed);
        }
    }
}