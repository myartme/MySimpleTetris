using UnityEngine;
using View.GUI.Scheme.ColorStyleWrappers;

namespace View.GUI.Buttons
{
    public class Slider : MonoBehaviour
    {
        [SerializeField] private ColorImageWrapper _background;
        [SerializeField] private ColorImageWrapper _handle;
        [SerializeField] private ColorImageWrapper _fill;

        /*private void OnEnable()
        {
            ColorTheme.OnCurrentTheme += OnSetCurrentTheme;
        }

        private void OnDisable()
        {
            ColorTheme.OnCurrentTheme -= OnSetCurrentTheme;
        }

        private void OnSetCurrentTheme()
        {
            _background.Color = ColorTheme.CurrentTheme.Shadow;
            _handle.Color = ColorTheme.CurrentTheme.Main;
            _fill.Color = ColorTheme.CurrentTheme.Main;
        }*/
    }
}