namespace Game
{
    public class BranchActionHat : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsHat;
        }
    }
}