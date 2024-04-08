namespace Game
{
    public class BranchActionNotCapturedWorld : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsNotCapturedWorld;
        }
    }
}