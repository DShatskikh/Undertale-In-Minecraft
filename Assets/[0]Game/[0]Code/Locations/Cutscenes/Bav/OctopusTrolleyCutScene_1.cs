using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
    public class OctopusTrolleyCutScene_1 : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _playableDirector;

        [SerializeField]
        private GameObject _science1, _science2;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(20);

            while (!GameData.CharacterController.enabled)
            {
                yield return new WaitForSeconds(4);
            }
            
            GameData.CharacterController.enabled = false;

            _playableDirector.Play();
            var isEndTimeline = false;
            _playableDirector.stopped += (_) => isEndTimeline = true;

            yield return new WaitUntil(() => isEndTimeline);

            _science1.SetActive(false);
            _science2.SetActive(true);
            
            GameData.CharacterController.enabled = true;
        }
    }
}