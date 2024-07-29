using YG;

namespace Game
{
    public class BranchActionIsOneOrMoreEnd : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsOneOrMoreEnd;
        }
    }
}