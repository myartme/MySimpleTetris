using Engine;

namespace View.TextField
{
    public class Score : BaseText<Score>
    {
        protected override string DefaultText => "Score: ";
        private void Start()
        {
            Game.OnTotalPoints += UpdateCountText;
        }
    }
}