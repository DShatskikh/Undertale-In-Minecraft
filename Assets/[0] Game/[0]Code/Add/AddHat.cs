using YG;

namespace Game
{
    public class AddHat : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsHat = true;
            GameData.Character.HatPoint.HatShowAndHide(true);
        }
    }
}