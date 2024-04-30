using UnityEngine;
using View.GUI.Grid;

namespace View.GUI.Screen
{
    public class GameOverScreen : BaseScreen<GameOverScreen>
    {
        [SerializeField] private TotalTableGrid _totalTableGrid;

        public void ShowScreen(bool isShowScreen)
        {
            SetActiveScreen(isShowScreen);
            if (isShowScreen)
            {
                _totalTableGrid.GenerateTotalTable();
            }
        }
    }
}