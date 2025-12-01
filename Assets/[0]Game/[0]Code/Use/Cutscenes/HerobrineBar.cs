using UnityEngine;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public sealed class HerobrineBar : UseObject
    {
        [SerializeField]
        private Replica[] _firstSpeak; // Впервые разговариваем с Херобрином
        
        [SerializeField]
        private Replica[] _secondSpeak; // Если не поговорили с Нотчем
        
        [SerializeField]
        private Replica[] _capturedWorldSpeak; // Если захватили Майнкрафт
        
        [SerializeField]
        private Replica[] _capturedWorldSpeak_2; //  Если захватили Майнкрафт и поговорить 2 раз
        
        [SerializeField]
        private LocalizedString[] _notCapturedWorld; // Если не стали захватывать Майнкрафт

        [SerializeField]
        private GameObject _herobrine;
        
        private void OnEnable()
        {
            if (YandexGame.savesData.IsNotCapturedWorld)
            {
                _herobrine.SetActive(false);
            }
        }

        public override void Use()
        {
            if (!YandexGame.savesData.IsSpeakHerobrine)
            {
                GameData.Dialog.SetReplicas(_firstSpeak);
                YandexGame.savesData.IsSpeakHerobrine = true;
            }
            else
            {
                if (YandexGame.savesData.IsCapturedWorld)
                {
                    if (YandexGame.savesData.GetInt("SpeakHerobrineCapsuledWorld") != 1)
                    {
                        GameData.Dialog.SetReplicas(_capturedWorldSpeak);
                        YandexGame.savesData.SetInt("SpeakHerobrineCapsuledWorld", 1);
                    }
                    else
                    {
                        GameData.Dialog.SetReplicas(_capturedWorldSpeak_2);
                    }
                }
                else if (YandexGame.savesData.IsNotCapturedWorld)
                {
                    GameData.Monolog.Show(_notCapturedWorld);
                }
                else
                {
                    GameData.Dialog.SetReplicas(_secondSpeak);
                }
            }
        }
    }
}