using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game
{
    public class ModeratorCutscene : BaseCutscene
    {
        [SerializeField]
        private Transform _cameraTarget;
        
        [SerializeField]
        private Transform _cameraFinishTarget;
        
        [SerializeField]
        private GameObject _explosion;
        
        [SerializeField]
        private GameObject _hole;
        
        [SerializeField]
        private SpriteRenderer _joe, _sasha;

        [SerializeField]
        private Sprite _sashaScared;
        
        [SerializeField]
        private GameObject _moderator;

        [SerializeField]
        private Transform _moderatorTarget;
        
        [SerializeField]
        private GameObject _portal;
        
        [SerializeField]
        private GameObject _portalExplosion;

        [SerializeField]
        private Transform _moderatorEndDialogTarget;

        protected override IEnumerator AwaitCutscene()
        {
            GameData.Saver.IsSave = false;
            
            GameData.CharacterController.enabled = false;
            _cameraTarget.transform.position = GameData.CinemachineVirtualCamera.transform.position;

            GameData.CinemachineVirtualCamera.Follow = _cameraTarget;
            
            var cameraMoveToPointCommand = new MoveToPointCommand(_cameraTarget.transform, _cameraTarget.transform.position.SetX(_cameraFinishTarget.position.x), 3);
            yield return cameraMoveToPointCommand.Await();

            yield return new WaitForSeconds(2);

            yield return AwaitDialog();

            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
            _explosion.SetActive(true);
            
            yield return new WaitForSeconds(0.25f);
            
            _hole.SetActive(true);
            
            yield return new WaitForSeconds(0.25f);
            
            _explosion.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            _joe.flipX = true;
            var sashaNormal = _sasha.sprite;
            _sasha.sprite = _sashaScared;

            yield return new WaitForSeconds(0.5f);

            _moderator.SetActive(true);
            
            var moderatorMoveToPointCommand = new MoveToPointCommand(_moderator.transform, _moderatorTarget.position, 1);
            yield return moderatorMoveToPointCommand.Await();
            yield return new WaitForSeconds(0.5f);

            _joe.flipX = false;
            
            yield return new WaitForSeconds(0.5f);
            
            yield return AwaitDialog();
            
            yield return new WaitForSeconds(1);
            
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
            _portal.SetActive(false);
            _portalExplosion.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _portalExplosion.SetActive(false);
            
            yield return new WaitForSeconds(1);
            _joe.flipX = true;
            
            yield return new WaitForSeconds(0.5f);
            
            yield return AwaitDialog();
            
            var moderatorEndDialogMoveToPointCommand = new MoveToPointCommand(_moderator.transform, _moderatorEndDialogTarget.position, 2);
            yield return moderatorEndDialogMoveToPointCommand.Await();

            yield return new WaitForSeconds(1);
            
            yield return AwaitDialog();
            
            yield return new WaitForSeconds(0.5f);
            
            _joe.flipX = false;
            _sasha.sprite = sashaNormal;
            
            var cameraMoveToStartPointCommand = new MoveToPointCommand(_cameraTarget.transform, GameData.CharacterController.transform.position, 3);
            yield return cameraMoveToStartPointCommand.Await();
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;
            
            yield return new WaitForSeconds(2);
            
            yield return AwaitDialog();
            
            var dictionary = new Dictionary<string, string>() { {"Ends","Strange"} };
            YandexMetrica.Send("Ends", dictionary);
            
            GameData.Saver.Reset();
        }
    }
}