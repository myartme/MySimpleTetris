using Engine;

namespace View.TextField
{
    public class Level : BaseText<Level>
    {
        protected override string DefaultText => "Level: ";
        private void Start()
        {
            Game.OnLevelChange += UpdateCountText;
        }
    }
}