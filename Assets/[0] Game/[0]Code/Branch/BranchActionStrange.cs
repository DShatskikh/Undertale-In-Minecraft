namespace Game
{
    public class BranchActionStrange : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsStrangeEnd;
        }
    }
}