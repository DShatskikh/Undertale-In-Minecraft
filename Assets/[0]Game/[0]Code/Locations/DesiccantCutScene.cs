using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DesiccantCutScene : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _desiccant;

        [SerializeField]
        private SpriteRenderer _light;

        [SerializeField]
        private SpriteRenderer _explosion;

        [SerializeField]
        private PlaySound _explosionPlay;
        
        [SerializeField]
        private float _speed;

        [SerializeField]
        private UnityEvent _event;

        private IEnumerator Start()
        {
            _desiccant.color = Color.black;
            _light.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);

            var process = 0f;

            while (process < 1f)
            {
                _desiccant.color = Color.Lerp(Color.black, Color.white, process);
                _light.color = Color.Lerp(Color.white, Color.clear, process);
                process += Time.deltaTime * _speed;
                yield return null;
            }
            
            _explosion.gameObject.SetActive(true);
            _explosionPlay.Play();
            yield return new WaitForSeconds(0.5f);
            _explosion.gameObject.SetActive(false);
            
            _event.Invoke();
        }
    }
}