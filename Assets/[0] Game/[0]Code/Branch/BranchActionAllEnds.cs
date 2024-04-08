namespace Game
{
    public class BranchActionAllEnds : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsGoodEnd && GameData.IsBadEnd && GameData.IsStrangeEnd;
        }
    }
}