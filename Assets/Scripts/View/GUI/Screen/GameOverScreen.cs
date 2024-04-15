namespace View.GUI.Screen
{
    public class GameOverScreen : BaseScreen<GameOverScreen>
    {
        protected override void Start()
        {
            base.Start();
            SetActiveScreen(false);
        }

        public void ShowScreen(bool isShowScreen)
        {
            SetActiveScreen(isShowScreen);
        }
    }
}