using YG;

namespace Game
{
    public class AddGoodEnd : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.IsGoodEnd = true;
            GameData.CurrentEnd = End.Good;
        }
    }
}