namespace View.Screen
{
    public class GameStart : BaseScreen<GameStart>
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
    }
}