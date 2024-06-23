using YG;

namespace Game
{
    public class AddNotIntroduction : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsNotIntroduction = true;
        }
    }
}