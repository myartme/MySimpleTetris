using Engine;

namespace View.GUI.Screen
{
    public class GamePauseScreen : BaseScreen<GamePauseScreen>
    {
        public void ShowScreen(bool isShowScreen)
        {
            SetShowScreen(isShowScreen);
        }
    }
}