using System;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game
{
    public class EndingsManager : MonoBehaviour
    {
        [SerializeField]
        public GameObject _badEnd;
        
        [SerializeField]
        public GameObject _goodEnd;
        
        [SerializeField]
        public GameObject _strangeEnd;

        [SerializeField]
        private AudioClip _phoneClip;

        [SerializeField]
        private GameObject _darkPanel;
        
        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;
        
        public void End(Endings ending)
        {
            GameData.LocationsManager.gameObject.SetActive(false);
            GameData.CompanionsManager.gameObject.SetActive(false);

            StartCoroutine(AwaitEnd(ending));
        }

        private IEnumerator AwaitEnd(Endings ending)
        {
            bool isEnd = false;
            
            switch (ending)
            {
                case Endings.Bad:
                    _badEnd.SetActive(true);
                    break;
                case Endings.Good:
                    _goodEnd.SetActive(true);
                    break;
                case Endings.Strange:
                    _strangeEnd.SetActive(true);
                    break;
                case Endings.BavGood:
                    _darkPanel.SetActive(true);
                    yield return new WaitForSeconds(2);
                    GameData.EffectSoundPlayer.Play(_phoneClip);
                    _dialogueSystemTrigger.OnUse();
                    
                    EventBus.CloseDialog += () => isEnd = true;
                    yield return new WaitUntil(() => isEnd);

                    yield return new WaitForSeconds(2);
            
                    SceneManager.LoadScene(0);
                    break;
                case Endings.BavGenocide:
                    _darkPanel.SetActive(true);
                    yield return new WaitForSeconds(2);
                    GameData.EffectSoundPlayer.Play(_phoneClip);
                    _dialogueSystemTrigger.OnUse();
                    
                    EventBus.CloseDialog += () => isEnd = true;
                    yield return new WaitUntil(() => isEnd);

                    yield return new WaitForSeconds(2);
            
                    SceneManager.LoadScene(0);
                    break;
                case Endings.BavStrange:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ending), ending, null);
            }
        }
    }
}