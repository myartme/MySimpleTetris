using UnityEngine;

namespace View.GUI.Scheme
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