using YG;

namespace Game
{
    public class AddNetherStar : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsNetherStar = true;
        }

        public void Remove()
        {
            YandexGame.savesData.IsNetherStar = false;
        }
    }
}