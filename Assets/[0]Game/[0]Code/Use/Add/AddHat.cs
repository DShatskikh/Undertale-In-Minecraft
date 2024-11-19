using YG;

namespace Game
{
    public class AddHat : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsCake = true;
            GameData.CharacterController.HatPoint.HatShow(true);
        }
    }
}