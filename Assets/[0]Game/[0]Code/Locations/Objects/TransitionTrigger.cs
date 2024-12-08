using System.Collections;
using UnityEngine;

namespace Game
{
    public class TransitionTrigger : MonoBehaviour
    {
        [SerializeField]
        private string _nextLocationIndex = "BavWorld";

        [SerializeField]
        private int _pointIndex;
        
        [SerializeField]
        private AudioClip _audioClip;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController characterController))
            {
                GameData.CoroutineRunner.StartCoroutine(AwaitTransition(characterController));
            }
        }

        private IEnumerator AwaitTransition(CharacterController characterController)
        {
            characterController.enabled = false;
            yield return GameData.TransitionScreen.AwaitShow();
            
            if (_audioClip)
                GameData.EffectSoundPlayer.Play(_audioClip);
            
            GameData.LocationsManager.SwitchLocation(_nextLocationIndex, _pointIndex);
            yield return GameData.TransitionScreen.AwaitHide();
            characterController.enabled = true;
        }
    }
}
