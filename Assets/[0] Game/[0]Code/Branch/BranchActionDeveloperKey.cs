namespace Game
{
    public class BranchActionDeveloperKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.IsDeveloperKey;
        }
    }
}