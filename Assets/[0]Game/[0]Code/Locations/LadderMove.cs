using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class LadderMove : MonoBehaviour
    {
        [SerializeField]
        private Transform _point;

        private Coroutine _coroutine;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController characterController))
            {
                if (GameData.CharacterController.enabled && enabled && gameObject.activeSelf)
                    _coroutine = StartCoroutine(AwaitMove());   
            }
        }

        private void OnDisable()
        {
            if (_coroutine != null && GameData.CharacterController != null)
            {
                StopCoroutine(_coroutine);
                
                GameData.CharacterController.enabled = true;
                GameData.SaveLoadManager.IsSave = true;
            }
        }

        private IEnumerator AwaitMove()
        {
            GameData.SaveLoadManager.IsSave = false;
            
            var character = GameData.CharacterController;

            character.GetComponent<Collider2D>().enabled = false;
            character.enabled = false;
            character.Model.SetSpeed(3);

            var distance = Vector3.Distance(character.transform.position, _point.position);
            var moveToPointCommand = new MoveToPointCommand(character.transform, _point.position, distance / 4);
            yield return moveToPointCommand.Await();
            
            character.enabled = true;
            character.GetComponent<Collider2D>().enabled = true;
            GameData.SaveLoadManager.IsSave = true;
        }
    }
}