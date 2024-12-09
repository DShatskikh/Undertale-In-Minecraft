using YG;

namespace Game
{
    public class BranchActionAllEnds : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsGoodEnd && YandexGame.savesData.IsBadEnd && YandexGame.savesData.IsStrangeEnd;
        }
    }
}