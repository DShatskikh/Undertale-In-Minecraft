using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Minicotic : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private Sprite _sprite1, _sprite2;
        
        [SerializeField]
        private Transform _startPoint, _endPoint;
        
        private IEnumerator Start()
        {
            while (true)
            {
                for (int i = 0; i < Random.Range(2, 6); i++)
                {
                    _spriteRenderer.sprite = _sprite1;
                    yield return new WaitForSeconds(0.25f);
                    _spriteRenderer.sprite = _sprite2;
                    yield return new WaitForSeconds(0.25f);
                }
     
                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    var process = 0f;

                    while (process < 1)
                    {
                        process += Time.deltaTime * 3;
                        _transform.position = Vector2.Lerp(_startPoint.position, _endPoint.position, process);
                        yield return null;
                    }
                    
                    process = 0f;
            
                    while (process < 1)
                    {
                        process += Time.deltaTime * 3;
                        _transform.position = Vector2.Lerp(_endPoint.position, _startPoint.position, process);
                        yield return null;
                    }
                }
                
                //yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));
            }
        }
    }
}