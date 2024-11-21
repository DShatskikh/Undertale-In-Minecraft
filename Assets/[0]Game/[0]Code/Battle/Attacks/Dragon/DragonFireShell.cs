using System.Collections;
using UnityEngine;

namespace Game
{
    public class DragonFireShell : Shell
    {
        [SerializeField]
        private ParticleSystem _particleSystem;
        
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.3f);
            _particleSystem.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            _particleSystem.gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position += transform.right * -1 * Time.deltaTime * transform.localScale.x * 3;
        }
    }
}