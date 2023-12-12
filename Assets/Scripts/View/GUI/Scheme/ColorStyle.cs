using UnityEngine;
using UnityEngine.UI;

namespace View.GUI.Scheme
{
    public abstract class ColorStyleGraphic<T> : MonoBehaviour where T : Graphic
    {
        private T _graphic;
        protected Color32 color = new(255, 198, 76, 220);

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
                _graphic.color = color;
            }
        }
    }
}
