using YG;

namespace Game
{
    public class AddStrangeEnd : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsStrangeEnd = true;
            GameData.CurrentEnd = End.Strange;
        }
    }
}