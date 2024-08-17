using YG;

namespace Game
{
    public class AddHat : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsHat = true;
            GameData.CharacterController.HatPoint.HatShowAndHide(true);
        }
    }
}