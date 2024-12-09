using YG;

namespace Game
{
    public class AddYouHealthy : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsYouHealthy = true;
        }
    }
}