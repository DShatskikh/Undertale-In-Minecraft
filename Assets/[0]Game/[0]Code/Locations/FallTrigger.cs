using System.Collections;
using UnityEngine;

namespace Game
{
    public class FallTrigger : MonoBehaviour
    {
        [SerializeField]
        private float _targetY;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController characterController))
            {
                GetComponent<Collider2D>().enabled = false;
                StartCoroutine(AwaitFall());
            }
        }

        private IEnumerator AwaitFall()
        {
            GameData.CharacterController.enabled = false;
            GameData.CharacterController.GetComponent<Collider2D>().enabled = false;
            GameData.CharacterController.Model.IsFly = true;

            var moveToPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, GameData.CharacterController.transform.position.SetY(_targetY), 3f);
            yield return moveToPointCommand.Await();
            
            GameData.CharacterController.Model.IsFly = false;
            GameData.CharacterController.GetComponent<Collider2D>().enabled = true;
            GameData.CharacterController.enabled = true;
        }
    }
}