using YG;

namespace Game
{
    public class BranchActionCheckPrisonKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsPrisonKey;
        }
    }
}