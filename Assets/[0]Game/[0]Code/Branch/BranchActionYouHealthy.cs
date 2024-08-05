using YG;

namespace Game
{
    public class BranchActionYouHealthy : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsYouHealthy;
        }
    }
}