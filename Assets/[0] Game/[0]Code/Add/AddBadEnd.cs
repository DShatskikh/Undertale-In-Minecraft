namespace Game
{
    public class AddBadEnd : AddBase
    {
        public override void Use()
        {
            GameData.IsBadEnd = true;
        }
    }
}