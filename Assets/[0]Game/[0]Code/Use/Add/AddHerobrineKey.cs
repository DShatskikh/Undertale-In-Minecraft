using YG;

namespace Game
{
    public class AddHerobrineKey : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsHerobrineKey = true;
        }
    }
}