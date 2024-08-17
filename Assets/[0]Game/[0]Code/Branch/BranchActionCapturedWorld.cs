using YG;

namespace Game
{
    public class BranchActionCapturedWorld : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsCapturedWorld;
        }
    }
}