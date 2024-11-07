using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class StonePuzzle : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent(out CharacterController characterController))
            {
                var direction = DirectionExtensions.GetDirection4((transform.position - characterController.transform.position).normalized);
                print(direction);

                /*
                 *Raycast(
      Vector2 origin,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results)
                 * 
                 */
                
                ContactFilter2D contactFilter = new ContactFilter2D();
                RaycastHit2D[] results = new RaycastHit2D[20];
                
                Physics2D.Raycast(transform.position, direction, contactFilter, results, 1.2f);
                
                if (results[1].transform == null)
                    StartCoroutine(AwaitMove(direction));
            }
        }

        private IEnumerator AwaitMove(Vector2 direction)
        {
            GameData.CharacterController.enabled = false;
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            var moveToPointCommand = new MoveToPointCommand(transform, transform.position + new Vector3(direction.x, direction.y * 0.7f, 0), 0.5f);
            yield return moveToPointCommand.Await();
            GameData.CharacterController.enabled = true;
        }
    }
}