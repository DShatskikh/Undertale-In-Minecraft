using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
    public class OctopusTrolleyCutScene_1 : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _playableDirector;
        
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

            Lua.Run("Variable[\"BavStopState\"] = 1");

            GameData.CharacterController.enabled = true;
        }
    }
}