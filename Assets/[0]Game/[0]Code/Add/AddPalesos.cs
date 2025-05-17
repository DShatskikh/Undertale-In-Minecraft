namespace Game
{
    public class AddPalesos : AddBase
    {
        public override void Use()
        {
            GameData.Palesos += 1;
        }
    }
}