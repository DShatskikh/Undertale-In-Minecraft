using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class EnderCrystalBarrier : MonoBehaviour
    {
        [SerializeField]
        private MMF_Player _shaking;
        
        [SerializeField]
        private EnderPost _post;
        
        private EnderCrystal _enderCrystal;
        
        public void Show(EnderCrystal enderCrystal)
        {
            gameObject.SetActive(true);
            _enderCrystal = enderCrystal;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var direction = (other.contacts[0].point - (Vector2)GameData.CharacterController.transform.position).normalized;
            StartCoroutine(AwaitAnimation(direction));
        }

        private IEnumerator AwaitAnimation(Vector2 direction)
        {
            GameData.CharacterController.enabled = false;
            
            _enderCrystal.transform.SetParent(_post.transform);
            
            var moveToDirectionCommand = new MoveToPointCommand(_enderCrystal.transform, _enderCrystal.transform.position.AddY(0.5f) + (Vector3)direction, 0.4f);
            yield return moveToDirectionCommand.Await();

            _enderCrystal.SortingGroup.sortingOrder = 1;
            
            if (direction.y > 0.5f)
                _enderCrystal.SortingGroup.sortingOrder = -1;
            
            var moveToDirectionCommand1 = new MoveToPointCommand(_enderCrystal.transform, _enderCrystal.transform.position.AddY(-1.5f) + (Vector3)direction, 0.3f);
            yield return moveToDirectionCommand1.Await();
            
            var moveToDownCommand = new MoveToPointCommand(_enderCrystal.transform, _enderCrystal.transform.position.AddY(-15f) + (Vector3)direction, 2.5f);
            yield return moveToDownCommand.Await();

            _enderCrystal.gameObject.SetActive(false);
            
            yield return _shaking.PlayFeedbacksCoroutine(Vector3.zero);

            _post.DeactivateCrystal();
            
            gameObject.SetActive(false);
            GameData.CharacterController.enabled = true;
        }
    }
}