using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
    public class OctopusTrolleyCutScene_2 : MonoBehaviour
    {
        [SerializeField]
        private Transform _octopusTransform;
        
        [SerializeField]
        private PlayableDirector _playableDirector;

        [SerializeField]
        private GameObject _select, _finishMonolog;
        
        [SerializeField]
        private AudioClip _audioClip;

        [SerializeField]
        private AudioClip _endClip;
        
        private IEnumerator Start()
        {
            GameData.CharacterController.enabled = false;

            //GameData.CinemachineVirtualCamera
            
            var characterMoveToOctopus = new MoveToPointCommand(GameData.CharacterController.transform, _octopusTransform.position.AddY(0.4f).AddX(0.4f), 0.5f);
            yield return characterMoveToOctopus.Await();
            
            GameData.CharacterController.transform.SetParent(_octopusTransform);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            
            GameData.MusicPlayer.Play(_audioClip);

            _playableDirector.Play();
            var isEndTimeline = false;
            _playableDirector.stopped += (_) => isEndTimeline = true;

            yield return new WaitUntil(() => isEndTimeline);
            
            //var octopusMoveToPoint = new MoveToPointCommand(_octopusTransform, _nextPoint.position, 6f);
            //yield return octopusMoveToPoint.Await();
            
            GameData.CharacterController.transform.SetParent(null);
            
            var CharacterMoveToFinish = new MoveToPointCommand(GameData.CharacterController.transform, _octopusTransform.position.AddY(-1.2f), 0.5f);
            yield return CharacterMoveToFinish.Await();
            
            _select.gameObject.SetActive(false);
            _finishMonolog.gameObject.SetActive(true);
            
            GameData.MusicPlayer.Play(_endClip);
            
            GameData.CharacterController.enabled = true;
        }
    }
}