using UnityEngine;

namespace View.GUI.Scheme.ColorStyleWrappers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorSpriteRendererWrapper : ColorStyleGraphic<ColorSpriteRendererWrapper>, IColorable
    {
        public Color Color
        {
            get => GetComponent<SpriteRenderer>().color;
            set => GetComponent<SpriteRenderer>().color = value;
        }
    }
}