using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnderPortal : MonoBehaviour
    {
        [SerializeField]
        private ExitLocation _exitLocation;
        
        public void Use()
        {
            StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            GameData.CharacterController.enabled = false;
            GameData.CharacterController.View.SetOrderInLayer(1);
            yield return new WaitForSeconds(0.5f);

            var moveToBackPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, GameData.CharacterController.transform.position.AddY(-0.5f), 0.5f);
            yield return moveToBackPointCommand.Await();
            
            yield return new WaitForSeconds(1f);
            
            var moveToUpPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, transform.position, 1f);
            yield return moveToUpPointCommand.Await();

            GameData.CharacterController.View.SetOrderInLayer(0);
            GameData.CharacterController.enabled = true;
            _exitLocation.Exit();
        }
    }
}