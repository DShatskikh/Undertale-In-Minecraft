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
            yield return new WaitForSeconds(0.5f);

            var moveToUpPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, transform.position, 1f);
            yield return moveToUpPointCommand.Await();

            GameData.CharacterController.enabled = true;
            _exitLocation.Exit();
        }
    }
}