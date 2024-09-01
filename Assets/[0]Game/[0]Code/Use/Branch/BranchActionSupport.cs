using YG;

namespace Game
{
    public class BranchActionSupport : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsBuySupport;
        }
    }
}