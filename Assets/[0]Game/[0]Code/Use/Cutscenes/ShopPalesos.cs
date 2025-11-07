using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public sealed class ShopPalesos : UseObject
    {
        [SerializeField]
        private Replica[] _replicas_1;
        // Бро купи Пылесос если купишь его, то тебе откроется новый контент в следующем прохождении

        [SerializeField]
        private LocalizedString _replicas_selected;
        // Купить пылесос? (За 1 рекламу)
        
        [SerializeField]
        private LocalizedString[] _replicas_Yes;
        // По какой-то неведомой причине вы получили 4953 пылесоса
        // Кажется вы забыли посмотреть рекламу
        
        [SerializeField]
        private LocalizedString[] _replicas_No;
        // Вы отказалить от этой сомнительной сделки
        
        [SerializeField]
        private Replica[] _replicas_3;
        // Бро, спасибо за покупку
        // Поверь ты не пожалеешь о ней
        // Я позвоню тебе в конце
        // Мы в пылесосе!

        [SerializeField]
        private Replica[] _replicas_4;
        // Мы в пылесосе!

        [SerializeField]
        private AudioClip _buySfx;
        
        public override void Use()
        {
            if (YG.YandexGame.savesData.Palesos != 3)
            {
                GameData.Dialog.SetReplicas(_replicas_1);
                EventBus.CloseDialog += () =>
                {
                    GameData.Select.Show(_replicas_selected, () =>
                    {
                        GameData.EffectAudioSource.clip = _buySfx;
                        GameData.EffectAudioSource.Play();
                        
                        GameData.Monolog.Show(_replicas_Yes);
                        EventBus.CloseMonolog += () =>
                        {
                            StartCoroutine(AwaitBuy());
                        };
                    }, () =>
                    {
                        GameData.Monolog.Show(_replicas_No);
                    });
                };
            }
            else
            {
                GameData.Dialog.SetReplicas(_replicas_4);
            }
        }

        private IEnumerator AwaitBuy()
        {
            GameData.Character.enabled = false;
            yield return new WaitForSeconds(1);
            
            GameData.Dialog.SetReplicas(_replicas_3);
            EventBus.CloseDialog += () =>
            {
                YG.YandexGame.savesData.Palesos = 3;
            };
        }
    }
}