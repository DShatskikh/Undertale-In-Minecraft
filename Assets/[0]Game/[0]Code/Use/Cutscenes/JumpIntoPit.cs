using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class JumpIntoPit : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private Animator _animator;
        
        public void Jump()
        {
            StartCoroutine(Await());
        }

        private IEnumerator Await()
        {
            GameData.Character.enabled = false;
            GameData.Character.GetComponent<Collider2D>().enabled = false;
            FindFirstObjectByType<CinemachineVirtualCamera>().Follow = null;

            var delta = 0f;
            var startPosition = GameData.Character.transform.position;

            while (delta < 1f)
            {
                delta += Time.deltaTime;
                GameData.Character.transform.position = Vector2.Lerp(startPosition, _container.position, delta);
                yield return null;
            }

            GameData.Character.View.Flip(false);
            yield return new WaitForSeconds(1);
            GameData.Character.gameObject.SetActive(false);
            _animator.gameObject.SetActive(true);
            yield return new WaitForSeconds(6);
            GameData.Saver.Reset();
            GameData.IsFlyingMenu = true;
            SceneManager.LoadScene(0);
        }
    }
}