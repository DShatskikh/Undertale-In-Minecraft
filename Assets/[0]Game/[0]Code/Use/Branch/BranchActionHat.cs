using YG;

namespace Game
{
    public class BranchActionHat : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsCake;
        }
    }
}