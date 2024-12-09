using YG;

namespace Game
{
    public class BranchActionDeveloperKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsDeveloperKey;
        }
    }
}