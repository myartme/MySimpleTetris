using Engine;

namespace View.TextField
{
    public class TetrominoCount : BaseText<TetrominoCount>
    {
        protected override string DefaultText => "Tetromino Count: ";
        private void Start()
        {
            Game.OnTetrominoCompleted += UpdateCountText;
        }
    }
}