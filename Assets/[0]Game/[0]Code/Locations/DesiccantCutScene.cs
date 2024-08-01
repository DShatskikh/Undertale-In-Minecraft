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
        private UnityEvent _event;
        
        private IEnumerator Start()
        {
            _desiccant.color = Color.black;
            _light.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);

            var process = 0f;

            while (process < 1f)
            {
                _desiccant.color = Color.Lerp(Color.black, Color.white, process);
                _light.color = Color.Lerp(Color.white, Color.clear, process);
                process += Time.deltaTime;
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}