using System.Collections;
using UnityEngine;

namespace Game
{
    public class IntermediateCutscene : BaseCutscene
    {
        [SerializeField]
        private Transform _characterPoint;
        
        [SerializeField]
        private Transform _focusPoint;
        
        protected override IEnumerator AwaitCutscene()
        {
            GameData.CharacterController.enabled = false;
            
            var characterMoveToPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, _characterPoint.position, 1);
            yield return characterMoveToPointCommand.Await();
            
            var cameraMoveToPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, _focusPoint.position, 1);
            yield return cameraMoveToPointCommand.Await();

            yield return AwaitDialog();
            
            GameData.CharacterController.enabled = true;
        }
    }
}