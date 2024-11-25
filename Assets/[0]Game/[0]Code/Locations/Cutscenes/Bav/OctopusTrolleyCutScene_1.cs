using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Playables;
using YG;

namespace Game
{
    public class OctopusTrolleyCutScene_1 : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _playableDirector;

        [SerializeField]
        private Transform _focusPoint;

        private IEnumerator Start()
        {
            GameData.Saver.IsSave = false;
            yield return new WaitForSeconds(10);

            while (!GameData.CharacterController.enabled)
            {
                yield return new WaitForSeconds(4);
            }
            
            GameData.CharacterController.enabled = false;
            GameData.CinemachineVirtualCamera.Follow = _focusPoint;

            var trolleyPoint = _focusPoint.transform.position;
            _focusPoint.transform.position = GameData.CharacterController.transform.position;
            
            var focusPointMoveTrolley = new MoveToPointCommand(_focusPoint.transform, trolleyPoint, 1f);
            yield return focusPointMoveTrolley.Await();
            
            _playableDirector.Play();
            var isEndTimeline = false;
            _playableDirector.stopped += (_) => isEndTimeline = true;

            yield return new WaitUntil(() => isEndTimeline);

            var focusPointMoveCharacter = new MoveToPointCommand(_focusPoint.transform, GameData.CharacterController.transform.position, 1f);
            yield return focusPointMoveCharacter.Await();
            
            Lua.Run("Variable[\"BavWorldStopState\"] = 1");

            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.View.transform;
            GameData.CharacterController.enabled = true;
            
            YandexMetrica.Send("TripOctopus");
            GameData.Saver.IsSave = true;
        }
    }
}