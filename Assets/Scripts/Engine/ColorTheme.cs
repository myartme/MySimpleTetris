using System;
using System.Collections.Generic;
using Save.Data.SaveDataElements;
using Service.Singleton;
using UnityEngine;
using View.Scene;

namespace Engine
{
    public class ColorTheme : AbstractSingleton<ColorTheme>
    {
        [SerializeField] private List<Sprite> blockVisualList;
        public static event Action OnCurrentTheme;
        public static event Action OnCurrentBlock;

        public ColorScheme.Theme CurrentTheme 
            => ColorScheme.GetTheme((ColorThemeType)CurrentThemeId);

        public int CurrentThemeId
        {
            get => _currentThemeId;
            private set
            {
                _currentThemeId = value;
                Save.ThemeId = _currentThemeId;
                OnCurrentTheme?.Invoke();
            }
        }
        
        public Sprite CurrentBlock => blockVisualList[CurrentBlockId];
        public int CurrentBlockId
        {
            get => _currentBlockId;
            private set
            {
                _currentBlockId = value;
                Save.BlockId = _currentBlockId;
                OnCurrentBlock?.Invoke();
            }
        }
        
        private VisualOptions Save => Saver.SaveData.Visual;

        private int _currentBlockId, _currentThemeId = -1;
        
        private void Awake()
        {
            GetComponent<ISingularObject>().Initialize();
        }
        
        private void Start()
        {
            LoadSaveData();
        }

        public void LoadSaveData()
        {
            CurrentThemeId = (int)Save.ThemeId;
            CurrentBlockId = (int)Save.BlockId;
        }

        public void IncrementBlockId()
        {
            if (CurrentBlockId == blockVisualList.Count - 1)
            {
                CurrentBlockId = 0;
            }
            else
            {
                CurrentBlockId++;
            }
        }
        
        public void DecrementBlockId()
        {
            if (CurrentBlockId == 0)
            {
                CurrentBlockId = blockVisualList.Count - 1;
            }
            else
            {
                CurrentBlockId--; 
            }
        }

        public void ChangeColorTheme(bool theme)
        {
            CurrentThemeId = theme ? 1 : 0;
        }
        
        private void OnEnable()
        {
            Saver.OnLoadData += LoadSaveData;
        }

        private void OnDisable()
        {
            Saver.OnLoadData -= LoadSaveData;
        }
    }
}