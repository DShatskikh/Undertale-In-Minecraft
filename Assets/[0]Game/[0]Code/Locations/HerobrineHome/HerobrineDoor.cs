using System.Collections;
using UnityEngine;

namespace Game
{
    public sealed class HerobrineDoor : MonoBehaviour
    {
        [SerializeField]
        private UseMonolog _yesMonolog;

        [SerializeField]
        private ExitLocation _exitLocation;
        
        [SerializeField]
        private GameObject _location;

        [SerializeField]
        private PlaySoundEffect _sfxClose;
        
        public void Use()
        {
            GameData.Startup.StartCoroutine(Await());
        }

        private IEnumerator Await()
        {
            GameData.MusicAudioSource.Stop();
            _location.SetActive(false);
            _sfxClose.Play();
            GameData.Character.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            _yesMonolog.Use();
            yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
            GameData.Character.gameObject.SetActive(true);
            _exitLocation.Exit();
        }
    }
}