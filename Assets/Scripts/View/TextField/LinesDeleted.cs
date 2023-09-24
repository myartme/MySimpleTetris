using Engine;

namespace View.TextField
{
    public class LinesDeleted : BaseText<LinesDeleted>
    {
        protected override string DefaultText => "Lines Deleted: ";
        private void Start()
        {
            Game.OnLinesDeleted += UpdateCountText;
        }
    }
}