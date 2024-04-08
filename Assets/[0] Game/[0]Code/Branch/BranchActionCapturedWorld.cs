namespace Game
{
    public class BranchActionCapturedWorld : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsCapturedWorld;
        }
    }
}