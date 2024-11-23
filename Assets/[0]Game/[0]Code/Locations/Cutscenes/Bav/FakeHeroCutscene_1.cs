using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutscene_1 : BaseCutscene
    {
        [SerializeField]
        private Transform _cameraTarget;
        
        [SerializeField]
        private Transform _cameraFinishTarget;
        
        [SerializeField]
        private EnemyBase _startBattleTrigger;

        protected override IEnumerator AwaitCutscene()
        {
            GameData.CharacterController.enabled = false;
            _cameraTarget.transform.position = _cameraTarget.transform.position.SetY(GameData.CinemachineVirtualCamera.transform.position.y);

            GameData.CinemachineVirtualCamera.Follow = _cameraTarget;

            var cameraMoveToPointCommand = new MoveToPointCommand(_cameraTarget.transform,
                _cameraTarget.transform.position.SetY(_cameraFinishTarget.position.y), 1);
            yield return cameraMoveToPointCommand.Await();

            yield return new WaitForSeconds(2);

            yield return AwaitDialog();
            GameData.CharacterController.enabled = false;
            
            _startBattleTrigger.StartBattle();

            Lua.Run("Variable[\"FakeHeroState\"] = 1");
        }
    }
}