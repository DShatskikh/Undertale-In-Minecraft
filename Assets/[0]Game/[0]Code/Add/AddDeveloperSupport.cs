using UnityEngine;
using YG;

namespace Game
{
    public class AddDeveloperSupport : AddBase
    {
        [SerializeField]
        private string _id;

        [SerializeField]
        private UseDialog _yesDialog;
        
        [SerializeField]
        private UseMonolog _noMonolog;
        
        public override void Use()
        {
            YandexGame.PurchaseSuccessEvent += OnBuy;
            YandexGame.PurchaseFailedEvent += OnFailedBuy;
            YandexGame.BuyPayments(_id);
        }

        private void UnSubscribe()
        {
            YandexGame.PurchaseSuccessEvent -= OnBuy;
            YandexGame.PurchaseFailedEvent -= OnFailedBuy;
        }

        private void OnBuy(string obj)
        {
            UnSubscribe();
            _yesDialog.Use();
        }

        private void OnFailedBuy(string obj)
        {
            UnSubscribe();    
            _noMonolog.Use(); 
        }
    }
}