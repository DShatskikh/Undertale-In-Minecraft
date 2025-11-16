using System.Collections;
using UnityEngine;

namespace Game
{
    public sealed class FlyingMenuAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameObject _canvas;
        
        private IEnumerator Start()
        { 
            if (!GameData.IsFlyingMenu) 
            {
                yield break;
            }
            
            _canvas.SetActive(false);
            GetComponent<Animator>().enabled = true;
            GameData.IsFlyingMenu = false;
            GameData.MusicAudioSource.Stop();
            yield return null;
            GameData.MusicAudioSource.Stop();
            yield return null;
            GameData.MusicAudioSource.Stop();
            yield return new WaitForSeconds(3);
            GameData.MusicAudioSource.Play();
            _canvas.SetActive(true);
        }
    }
}