using YG;

namespace Game
{
    public class BranchActionTelephone : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsTelephone;
        }
    }
}