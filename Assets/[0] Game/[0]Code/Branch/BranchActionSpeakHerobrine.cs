namespace Game
{
    public class BranchActionSpeakHerobrine : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsSpeakHerobrine;
        }
    }
}