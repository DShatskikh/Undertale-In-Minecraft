namespace Game
{
    public class AddNotIntroduction : AddBase
    {
        public override void Use()
        {
            GameData.IsNotIntroduction = true;
        }
    }
}