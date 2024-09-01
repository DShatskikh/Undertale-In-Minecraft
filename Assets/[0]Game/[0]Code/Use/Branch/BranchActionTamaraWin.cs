using YG;

namespace Game
{
    public class BranchActionTamaraWin : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.GetInt("Tamara") == 1;
        }
    }
}