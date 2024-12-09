using YG;

namespace Game
{
    public class BranchActionNotCapturedWorld : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsNotCapturedWorld;
        }
    }
}