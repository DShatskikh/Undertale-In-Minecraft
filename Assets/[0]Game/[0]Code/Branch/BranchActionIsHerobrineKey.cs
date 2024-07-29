using YG;

namespace Game
{
    public class BranchActionIsHerobrineKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsHerobrineKey;
        }
    }
}