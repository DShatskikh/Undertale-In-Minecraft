using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game
{
    public class GoodGameEnd : MonoBehaviour
    {
        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;

        [SerializeField]
        private DialogueSystemTrigger _fakeHeoDialog;
        
        public void Use(bool isFakeHero)
        {
            GameData.CoroutineRunner.StartCoroutine(AwaitUse(isFakeHero));
        }

        private IEnumerator AwaitUse(bool isFakeHero)
        {
            GameData.LocationsManager.gameObject.SetActive(false);
            GameData.CharacterController.gameObject.SetActive(false);

            yield return new WaitForSeconds(2);
            
            if (isFakeHero)
            {
                _fakeHeoDialog.OnUse();
            
                bool isEnd1 = false;
                EventBus.CloseDialog += () => isEnd1 = true;
                yield return new WaitUntil(() => isEnd1);
                
                yield return new WaitForSeconds(2);
            
                GameData.LocationsManager.gameObject.SetActive(true);
                GameData.LocationsManager.SwitchLocation("BavDream", 0);
            
                yield return new WaitForSeconds(7);
                GameData.MusicPlayer.Stop();
            }

            yield return new WaitForSeconds(1);

            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.PhoneSound);
            _dialogueSystemTrigger.OnUse();
            
            bool isEnd = false;
            EventBus.CloseDialog += () => isEnd = true;
            yield return new WaitUntil(() => isEnd);

            yield return new WaitForSeconds(2);

            var isGenocide = Lua.IsTrue("IsGenocide() == true");
            
            var dictionary = new Dictionary<string, string>() { {"Ends", isGenocide ? "Bad" : "Good"} };
            YandexMetrica.Send("Ends", dictionary);

            GameData.Saver.Reset();
        }
    }
}