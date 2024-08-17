using YG;

namespace Game
{
    public class AddCapturedWorld : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsCapturedWorld = true;
        }
    }
}