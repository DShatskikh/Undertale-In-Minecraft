using YG;

namespace Game
{
    public class AddNotCapturedWorld : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsNotCapturedWorld = true;
        }
    }
}