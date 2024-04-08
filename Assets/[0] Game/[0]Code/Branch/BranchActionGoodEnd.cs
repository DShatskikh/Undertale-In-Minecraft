namespace Game
{
    public class BranchActionGoodEnd : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsGoodEnd;
        }
    }
}