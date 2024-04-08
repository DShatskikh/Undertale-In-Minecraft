namespace Game
{
    public class BranchActionBadEnd : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsBadEnd;
        }
    }
}