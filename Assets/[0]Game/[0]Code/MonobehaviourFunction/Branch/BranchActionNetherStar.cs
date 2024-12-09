using YG;

namespace Game
{
    public class BranchActionNetherStar : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsNetherStar;
        }
    }
}