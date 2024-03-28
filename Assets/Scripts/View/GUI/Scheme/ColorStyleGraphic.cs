using Engine;
using UnityEngine;
using View.Scene;

namespace View.GUI.Scheme
{
    public abstract class ColorStyleGraphic<T> : MonoBehaviour where T : Component, IColorable
    {
        [SerializeField] private ColorElementType colorElementType;
        [SerializeField] private ColorTheme _colorTheme;
        private T _graphic;

        public ColorElementType ColorElementType
        {
            get => colorElementType;
            set
            {
                colorElementType = value;
                ApplyColor();
            }
        }
        
        protected void OnEnable()
        {
            ColorTheme.OnCurrentTheme += ApplyColor;
            ApplyColor();
        }
        
        protected void OnDisable()
        {
            ColorTheme.OnCurrentTheme -= ApplyColor;
        }

        protected virtual void OnValidate()
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
                var theme = _colorTheme != null ? _colorTheme.CurrentTheme : ColorScheme.GetTheme(-1);
                _graphic.Color = theme.GetColor(ColorElementType);
            }
        }
    }
}
