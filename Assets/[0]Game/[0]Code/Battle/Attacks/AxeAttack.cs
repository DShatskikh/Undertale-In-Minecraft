using System.Collections;
using UnityEngine;

namespace Game
{
    public class AxeAttack : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _warning;

        [SerializeField]
        private GameObject _axe;

        [SerializeField]
        private GameObject _light;

        [SerializeField]
        private AudioClip _breakSound, _warningSound;
        
        private IEnumerator Start()
        {
            _warning.SetActive(true);
            GameData.EffectSoundPlayer.Play(_warningSound);
            yield return new WaitForSeconds(1f);
            _warning.SetActive(false);
            _axe.SetActive(true);
            _axe.transform.position = _axe.transform.position.SetY(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);

            bool isEndMove = false;

            var y = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
            var moveCommand = new MoveToPointCommand(_axe.transform, _axe.transform.position.SetY(y), 1.5f);
            moveCommand.Execute(() => isEndMove = true);

            yield return new WaitUntil(() => isEndMove);

            if (_axe)
                _axe.SetActive(false);
            else
                yield break;

            _light.SetActive(true);
            var collider = _light.GetComponent<Collider2D>();
            collider.enabled = false;
            GameData.EffectSoundPlayer.Play(_breakSound);
            var spriteRenderer = _light.GetComponent<SpriteRenderer>();
            
            isEndMove = false;
            var transparencyCommand = new TransparencyCommand(spriteRenderer, 1, 0.5f);
            transparencyCommand.Execute(() => isEndMove = true);

            yield return new WaitUntil(() => isEndMove);
            
            if (collider)
                collider.enabled = true;
            
            yield return new WaitForSeconds(1f);
            
            if (collider)
                collider.enabled = false;
            
            isEndMove = false;
            transparencyCommand = new TransparencyCommand(spriteRenderer, 0, 0.5f);
            transparencyCommand.Execute(() => isEndMove = true);
            
            yield return new WaitUntil(() => isEndMove);

            if (_light)
                _light.SetActive(false);
        }
    }
}