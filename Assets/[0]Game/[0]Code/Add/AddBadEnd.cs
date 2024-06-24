using YG;

namespace Game
{
    public class AddBadEnd : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsBadEnd = true;
            GameData.CurrentEnd = End.Bad;
        }
    }
}