using YG;

namespace Game
{
    public class PurchaseManager
    {
        public PurchaseManager()
        {
            YandexGame.PurchaseSuccessEvent += OnBuy;
        }

        ~PurchaseManager()
        {
            YandexGame.PurchaseSuccessEvent -= OnBuy;
        }

        private void OnBuy(string id)
        {
            switch (id)
            {
                case "DeveloperSupport":
                    YandexGame.savesData.IsBuySupport = true;
                    break;
                default:
                    break;
            }
        }
    }
}