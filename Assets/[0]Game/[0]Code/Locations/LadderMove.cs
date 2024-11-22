using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class LadderMove : MonoBehaviour
    {
        [SerializeField]
        private Transform _point;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController characterController))
            {
                if (GameData.CharacterController.enabled)
                    StartCoroutine(AwaitMove());   
            }
        }

        private IEnumerator AwaitMove()
        {
            var character = GameData.CharacterController;

            character.enabled = false;
            character.Model.SetSpeed(3);

            var distance = Vector3.Distance(character.transform.position, _point.position);
            var moveToPointCommand = new MoveToPointCommand(character.transform, _point.position, distance / 4);
            yield return moveToPointCommand.Await();
            
            character.enabled = true;
        }
    }
}