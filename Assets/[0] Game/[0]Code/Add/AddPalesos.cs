using YG;

namespace Game
{
    public class AddPalesos : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.Palesos += 1;
        }
    }
}