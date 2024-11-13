using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GenocideGameEnd : MonoBehaviour
    {
        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;

        public void Use()
        {
            GameData.CoroutineRunner.StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            GameData.LocationsManager.gameObject.SetActive(false);
            GameData.CharacterController.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.PhoneSound);
            _dialogueSystemTrigger.OnUse();
            
            bool isEnd = false;
            EventBus.CloseDialog += () => isEnd = true;
            yield return new WaitUntil(() => isEnd);

            yield return new WaitForSeconds(2);
            
            SceneManager.LoadScene(0);
        }
    }
}