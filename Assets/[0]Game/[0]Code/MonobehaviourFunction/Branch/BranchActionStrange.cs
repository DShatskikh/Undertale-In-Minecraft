using YG;

namespace Game
{
    public class BranchActionStrange : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsStrangeEnd;
        }
    }
}