using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] 
        private Sprite _activeSprite;

        [SerializeField] 
        private Sprite _deactivateSprite;

        [SerializeField]
        private Transform _point;

        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private UnityEvent _event;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                StartCoroutine(AwaitJump());
                _spriteRenderer.sprite = _activeSprite;
            }
        }
        
        private IEnumerator AwaitJump()
        {
            var character = GameData.CharacterController;
            var characterTransform = character.transform;

            GameData.Saver.IsSavingPosition = false;
            character.enabled = false;
            character.GetComponent<Collider2D>().enabled = false;
            character.transform.SetParent(_parent);
            character.Model.SetFly(true);
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            
            var targetPosition = new Vector2((_point.position.x + characterTransform.position.x) / 2, _point.position.y + 1f);

            yield return new MoveToPointCommand(characterTransform, targetPosition, 1f).Await();
            _spriteRenderer.sprite = _deactivateSprite;
            yield return new MoveToPointCommand(characterTransform, _point.position, 0.5f).Await();
            
            character.Model.SetFly(false);
            character.GetComponent<Collider2D>().enabled = true;
            character.enabled = true;
            _event.Invoke();
        }
    }
}