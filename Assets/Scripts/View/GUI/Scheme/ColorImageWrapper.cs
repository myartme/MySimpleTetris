using UnityEngine;
using UnityEngine.UI;

namespace View.GUI.Scheme
{
    [RequireComponent(typeof(Image))]
    public class ColorImageWrapper : ColorStyleGraphic<ColorImageWrapper>, IColorable
    {
        public Color Color
        {
            get => GetComponent<Image>().color;
            set => GetComponent<Image>().color = value;
        }
    }
}