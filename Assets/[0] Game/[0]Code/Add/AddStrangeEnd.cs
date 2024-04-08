namespace Game
{
    public class AddStrangeEnd : AddBase
    {
        public override void Use()
        {
            GameData.IsStrangeEnd = true;
        }
    }
}