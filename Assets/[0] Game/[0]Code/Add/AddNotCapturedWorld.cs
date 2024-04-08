namespace Game
{
    public class AddNotCapturedWorld : AddBase
    {
        public override void Use()
        {
            GameData.IsNotCapturedWorld = true;
        }
    }
}