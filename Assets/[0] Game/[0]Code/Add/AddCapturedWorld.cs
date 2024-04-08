namespace Game
{
    public class AddCapturedWorld : AddBase
    {
        public override void Use()
        {
            GameData.IsCapturedWorld = true;
        }
    }
}