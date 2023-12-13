using TMPro;
using UnityEngine;

namespace View.GUI.Scheme
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ColorTextWrapper : ColorStyleGraphic<ColorTextWrapper>, IColorable
    {
        public Color Color
        {
            get => GetComponent<TextMeshProUGUI>().color;
            set => GetComponent<TextMeshProUGUI>().color = value;
        }
    }
}