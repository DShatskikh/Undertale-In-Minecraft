using YG;

namespace Game
{
    public class AddGoldTulip : AddBase
    {
        public override void Use()
        {
            YandexGame.savesData.GoldTulip += 1;
        }
    }
}