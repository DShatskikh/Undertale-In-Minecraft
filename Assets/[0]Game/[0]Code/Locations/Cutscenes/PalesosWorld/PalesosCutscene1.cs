using System.Collections;
using UnityEngine;

namespace Game
{
    public class PalesosCutscene1 : MonoBehaviour
    {
        [Header("Act1")]
        [SerializeField]
        private Transform _chickenStartPoint;
        
        [SerializeField]
        private Transform _basharTransform;
        
        [SerializeField]
        private Transform _basharStartPoint;

        [SerializeField]
        private Transform _basharMovePoint;
        
        [SerializeField]
        private Transform _cameraStartPoint;
        
        [SerializeField]
        private Replica[] _basharReplicas;
        
        [Header("Act2")]
        [SerializeField]
        private Transform _camera;
        
        [SerializeField]
        private Transform _cameraPoint;
        
        [SerializeField]
        private Replica[] _palesosReplicas;
        
        [Header("Act3")]
        [SerializeField]
        private Transform _basharPalesosPoint;
        
        private IEnumerator Start()
        {
            //if ()
            //    yield break;
            
            GameData.CompanionsManager.DeactivateAllCompanion();

            GameData.CharacterController.transform.position = _chickenStartPoint.position;
            _camera.position = _cameraStartPoint.position.SetZ(-10);

            GameData.CharacterController.gameObject.SetActive(false);
            _basharTransform.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(1);
            
            GameData.CharacterController.gameObject.SetActive(true);
            GameData.CharacterController.enabled = false;
            _basharTransform.gameObject.SetActive(true);
            _basharTransform.position = _basharStartPoint.position;
            
            var basharMoveCommand = new MoveToPointCommand(_basharTransform, _basharMovePoint.position, 1);
            yield return basharMoveCommand.Await();

            var basharDialogCommand = new DialogCommand(_basharReplicas, null, null);
            yield return basharDialogCommand.Await();
            GameData.CharacterController.enabled = false;
            
            var palesosMoveCommand = new MoveToPointCommand(_camera, _cameraPoint.position.SetZ(-10), 2);
            yield return palesosMoveCommand.Await();

            var palesosDialogCommand = new DialogCommand(_palesosReplicas, null, null);
            yield return palesosDialogCommand.Await();
            GameData.CharacterController.enabled = false;
            
            var basharMove2Command = new MoveToPointCommand(_basharTransform, _basharPalesosPoint.position, 4);
            yield return basharMove2Command.Await();
            
            GameData.CharacterController.enabled = true;
        }
    }
}