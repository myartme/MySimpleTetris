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
        [SerializeField] private GameObject creatableTetrominos;
        public static event Action OnCurrentTheme;
        
        public ColorScheme.Theme CurrentTheme 
            => ColorScheme.GetTheme((ColorThemeType)CurrentThemeId);

        public int CurrentThemeId
        {
            get => _currentThemeId;
            private set
            {
                _currentThemeId = value;
                
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
                Save.SetValueByName(_block, _currentBlockId);
            }
        }
        
        public OptionsSave Save
        {
            get => Saver.SaveData.options;
            private set => Saver.SaveData.options = value;
        }

        private readonly string _theme = "Theme",
            _block = "BlockType";

        private int _currentBlockId, _currentThemeId = -1;

        private void Start()
        {
            InitializeSaveData();
            LoadSaveData();
        }

        public void StoreSaveData()
        {
            Saver.Store.Save(Saver.SaveData);
            ChangeBlocks();
        }
        
        public void ResetSaveData()
        {
            Save = Saver.Store.Load()?.options;
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

        private void ChangeBlocks()
        {
            if(!creatableTetrominos) return;
            
            foreach (Transform tetromino in creatableTetrominos.transform)
            {
                foreach (Transform block in tetromino.transform)
                {
                    var sr = block.GetComponent<SpriteRenderer>();
                    sr.sprite = CurrentBlock;
                }
            }
        }
    }
}