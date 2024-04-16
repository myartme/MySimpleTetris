using System.Collections.Generic;
using GameFigures;
using UnityEngine;

namespace View.Scene
{
    public static class ColorScheme
    {
        public static readonly Dictionary<BlockType, Color32> BlockColors = new()
        {
            { BlockType.I, new Color32(50, 180, 180, 255) },
            { BlockType.T, new Color32(100, 50, 180, 255) },
            { BlockType.J, new Color32(50, 50, 180, 255) },
            { BlockType.L, new Color32(180, 100, 50, 255) },
            { BlockType.Z, new Color32(180, 50, 50, 255) },
            { BlockType.O, new Color32(180, 180, 50, 255) },
            { BlockType.S, new Color32(50, 180, 50, 255) }
        };

        public static readonly Color32 Shadow = new (100, 100, 100, 80);
        private static readonly Theme _light = new ();
        private static readonly Theme _dark = new ();
        private static readonly Theme _editor = new ();

        static ColorScheme()
        {
            _light.SetColor(ColorElementType.Background, new Color32(205, 190, 167, 255))
                .SetColor(ColorElementType.Main, new Color32(155, 100, 0, 255))
                .SetColor(ColorElementType.Shadow, new Color32(50, 48, 48, 255))
                .SetColor(ColorElementType.Active, new Color32(136, 36, 38, 255));
            
            _dark.SetColor(ColorElementType.Background, new Color32(10, 20, 40, 255))
                .SetColor(ColorElementType.Main, new Color32(154, 133, 88, 255))
                .SetColor(ColorElementType.Shadow, new Color32(50, 40, 30, 255))
                .SetColor(ColorElementType.Active, new Color32(255, 174, 0, 200));

            _editor.SetColor(ColorElementType.Background, new Color32(10, 20, 40, 255))
                .SetColor(ColorElementType.Main, new Color32(150, 150, 150, 255))
                .SetColor(ColorElementType.Shadow, new Color32(0, 0, 0, 255))
                .SetColor(ColorElementType.Active, new Color32(50, 50, 50, 255));
        }
        
        public static Theme GetTheme(int themeNumber)
        {
            return GetTheme((ColorThemeType)themeNumber);
        }
        
        public static Theme GetTheme(ColorThemeType colorThemeType)
        {
            if (colorThemeType == ColorThemeType.Dark)
            {
                return _dark;
            }
            if (colorThemeType == ColorThemeType.Light)
            {
                return _light;
            }
            
            return _editor;
        }
        
        public class Theme
        {
            private Color32 _background;
            private Color32 _main;
            private Color32 _shadow;
            private Color32 _active;

            public Color32 GetColor(ColorElementType colorElementType)
            {
                return colorElementType switch
                {
                    ColorElementType.Background => _background,
                    ColorElementType.Main => _main,
                    ColorElementType.Shadow => _shadow,
                    ColorElementType.Active => _active,
                    _ => Shadow
                };
            }

            public Theme SetColor(ColorElementType colorElementType, Color32 color)
            {
                switch (colorElementType)
                {
                    case ColorElementType.Background:
                        _background = color;
                        break;
                    case ColorElementType.Main:
                        _main = color;
                        break;
                    case ColorElementType.Shadow:
                        _shadow = color;
                        break;
                    case ColorElementType.Active:
                        _active = color;
                        break;
                }

                return this;
            }
        }
    }
    
    public enum ColorThemeType
    {
        Editor = -1,
        Dark = 0,
        Light = 1,
    }
    
    public enum ColorElementType
    {
        Background,
        Main,
        Shadow,
        Active
    }
}