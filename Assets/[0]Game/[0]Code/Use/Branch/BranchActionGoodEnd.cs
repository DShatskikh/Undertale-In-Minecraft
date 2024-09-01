using YG;

namespace Game
{
    public class BranchActionGoodEnd : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsGoodEnd;
        }
    }
}