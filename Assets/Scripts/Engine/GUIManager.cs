using System;
using System.Collections.Generic;
using UnityEngine;
using View.GUI;
using View.GUI.Screen;
using View.GUI.TextField;

namespace Engine
{
    public class GUIManager : MonoBehaviour
    {
        public static List<BaseText> TextFields = new ();

        private void OnValidate()
        {
            UpdateScore(0);
            UpdateLinesDeleted(0);
            UpdateTetrominoCount(0);
            UpdateLevel(0);
        }

        public void ShowMainMenu()
        {
            GameStartScreen.ClassInstance.ShowScreen(true);
        }
        
        public void HideMainMenu()
        {
            GameStartScreen.ClassInstance.ShowScreen(false);
        }
        
        public void ShowPauseScreen()
        {
            GamePauseScreen.ClassInstance.ShowScreen(true);
        }
        
        public void HidePauseScreen()
        {
            GamePauseScreen.ClassInstance.ShowScreen(false);
        }
        
        public void ShowGameOverScreen()
        {
            GameOverScreen.ClassInstance.ShowScreen(true);
        }
        
        public void HideGameOverScreen()
        {
            GameOverScreen.ClassInstance.ShowScreen(false);
        }

        public void ShowOptionScreen()
        {
            GamePauseScreen.ClassInstance.ShowScreen(true);
        }

        public void UpdateScore(int count)
        {
            UpdateTypeFiled(count, typeof(ScoreTextField));
        }
        
        public void UpdateLinesDeleted(int count)
        {
            UpdateTypeFiled(count, typeof(LinesDeletedTextField));
        }
        
        public void UpdateTetrominoCount(int count)
        {
            UpdateTypeFiled(count, typeof(TetrominoCountTextField));
        }
        
        public void UpdateLevel(int count)
        {
            UpdateTypeFiled(count, typeof(LevelTextField));
        }

        private void UpdateTypeFiled(int count, Type type)
        {
            TextFields.FindAll(item => item.GetType() == type)
                .ForEach(itemType => itemType.UpdateCountText(count));
        }
    }
}