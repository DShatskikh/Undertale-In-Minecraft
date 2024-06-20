namespace Game
{
    public class AddHat : AddBase
    {
        public override void Use()
        {
            GameData.IsHat = true;
            GameData.Character.HatPoint.HatShowAndHide(true);
        }
    }
}