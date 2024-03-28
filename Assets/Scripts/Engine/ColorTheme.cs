using System;
using System.Collections.Generic;
using Save.Data.Format;
using UnityEngine;
using View.Scene;

namespace Engine
{
    public class ColorTheme : MonoBehaviour
    {
        [SerializeField]private List<Sprite> blockVisualList;
        public static event Action OnCurrentTheme, OnCurrentBlock;
        
        public ColorScheme.Theme CurrentTheme 
            => ColorScheme.GetTheme((ColorThemeType)CurrentThemeId);

        public int CurrentThemeId
        {
            get => _currentThemeId;
            private set
            {
                _currentThemeId = value;
                if (_currentThemeId < (int)ColorThemeType.Editor)
                {
                    _currentThemeId = (int)ColorThemeType.Editor;
                }

                if (_currentThemeId > _maxThemeId)
                {
                    _currentThemeId = _maxThemeId;
                }
                
                Save.SetValueByName(_theme, _currentThemeId);
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
                if (_currentBlockId < 0)
                {
                    _currentBlockId = 0;
                }

                if (_currentBlockId > blockVisualList.Count - 1)
                {
                    _currentBlockId = blockVisualList.Count - 1;
                }
                
                Save.SetValueByName(_block, _currentBlockId);
                OnCurrentBlock?.Invoke();
            }
        }
        
        public OptionsSave Save
        {
            get => Game.SaveData.options;
            private set => Game.SaveData.options = value;
        }

        private readonly string _theme = "Theme",
            _block = "BlockType";

        private int _maxThemeId = 1, _currentBlockId, _currentThemeId = -1;

        private void Start()
        {
            InitializeSaveData();
            LoadSaveData();
        }

        public void StoreSaveData()
        {
            Game.Store.Save(Game.SaveData);
        }
        
        public void ResetSaveData()
        {
            Save = Game.Store.Load()?.options;
            LoadSaveData();
        }
        
        public void InitializeSaveData()
        {
            Save.AddToList(_theme, 0f);
            Save.AddToList(_block, 0f);
            StoreSaveData();
        }

        public void LoadSaveData()
        {
            CurrentThemeId = (int)Save.GetValue(_theme);
            CurrentBlockId = (int)Save.GetValue(_block);
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
    }
}