using YG;

namespace Game
{
    public class BranchActionHerobrineKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsHerobrineKey;
        }
    }
}