using UnityEngine;

namespace View.GUI.Scheme
{
    public abstract class ColorStyleGraphic<T> : MonoBehaviour where T : Component, IColorable
    {
        private T _graphic;
        //(255, 198, 76, 220);
        protected Color32 color = new(154, 133, 88, 255);

        private void OnValidate()
        {
            ApplyColor();
        }

        private void ApplyColor()
        {
            if (_graphic == null)
            {
                _graphic = GetComponent<T>();
            }

            if (_graphic != null)
            {
                _graphic.Color = color;
            }
        }
    }
}
