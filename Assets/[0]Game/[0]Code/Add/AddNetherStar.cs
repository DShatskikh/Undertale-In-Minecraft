using YG;

namespace Game
{
    public class AddNetherStar : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsNetherStar = true;
        }
    }
}