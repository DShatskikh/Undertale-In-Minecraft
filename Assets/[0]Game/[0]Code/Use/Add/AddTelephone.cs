using YG;

namespace Game
{
    public class AddTelephone : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsTelephone = true;
        }
    }
}