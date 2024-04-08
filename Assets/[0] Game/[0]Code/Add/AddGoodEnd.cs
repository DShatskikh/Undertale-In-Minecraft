namespace Game
{
    public class AddGoodEnd : AddBase
    {
        public override void Use()
        {
            GameData.IsGoodEnd = true;
        }
    }
}