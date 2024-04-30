using Engine;
using UnityEngine;
using View.Scene;

namespace View.GUI.Scheme
{
    public abstract class ColorStyleGraphic<T> : MonoBehaviour where T : Component, IColorable
    {
        [SerializeField] private ColorElementType colorElementType;
        private T _graphic;
        private ColorScheme.Theme _colorTheme;

        public ColorElementType ColorElementType
        {
            get => colorElementType;
            set
            {
                colorElementType = value;
                ApplyColor();
            }
        }

        private void Awake()
        {
            SetCurrentTheme();
        }
        
        protected void OnValidate()
        {
            SetCurrentTheme();
        }
        
        private void SetCurrentTheme()
        {
            if (_graphic == null)
            {
                _graphic = GetComponent<T>();
            }

            if (_graphic != null)
            {
                _colorTheme = ColorTheme.Instance != null 
                    ? ColorTheme.Instance.CurrentTheme 
                    : ColorScheme.GetTheme(-1);
                
                ApplyColor();
            }
        }

        private void ApplyColor()
        {
            _graphic.Color = _colorTheme.GetColor(ColorElementType);
        }
        
        protected void OnEnable()
        {
            ColorTheme.OnCurrentTheme += SetCurrentTheme;
        }
        
        protected void OnDisable()
        {
            ColorTheme.OnCurrentTheme -= SetCurrentTheme;
        }
    }
}
