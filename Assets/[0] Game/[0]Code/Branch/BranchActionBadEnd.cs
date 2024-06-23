using YG;

namespace Game
{
    public class BranchActionBadEnd : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsBadEnd;
        }
    }
}