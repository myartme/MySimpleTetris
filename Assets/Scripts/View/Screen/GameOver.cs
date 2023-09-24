﻿namespace View.Screen
{
    public class GameOver : BaseScreen<GameOver>
    {
        protected override void Awake()
        {
            base.Awake();
            ShowScreen(false);
        }
        
        public void ShowScreen(bool isShowScreen)
        {
            IsWindowSetActive = isShowScreen;
            SetActiveScreen(isShowScreen);
            GamePauseIfWindowIsActive();
        }
        
        public void RestartButton()
        {
            ShowScreen(false);
        }
    }
}