using YG;

namespace Game
{
    public class BranchActionSpeakHerobrine : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsSpeakHerobrine;
        }
    }
}