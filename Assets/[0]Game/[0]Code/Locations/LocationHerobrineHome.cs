using System.Collections;
using RimuruDev;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class LocationHerobrineHome : MonoBehaviour
    {
        [SerializeField] 
        private Transform _startPoint;

        [SerializeField] 
        private Replica[] _replicas;

        [SerializeField] 
        private Replica[] _replicas_2;

        [SerializeField] 
        private Replica[] _replicas_3;
        
        [SerializeField] 
        private UnityEvent _event, _event2;
        
        [SerializeField]
        private GameObject _mobileInput, _pcInput;

        [SerializeField]
        private GameObject _herobrine;

        [SerializeField]
        private Sprite _surpriseSprite;

        [SerializeField]
        private AudioClip _funnyMusic;
        
        private bool _isToFollow = false;
        
        private void OnEnable()
        {
            StartCoroutine(CutScene());
        }

        private void Update()
        {
            if (_isToFollow)
            {
                _herobrine.GetComponent<SpriteRenderer>().flipX =
                    GameData.Character.transform.position.x - _herobrine.transform.position.x < 0; // Херобрин смотрит на игрока
            }
        }

        private IEnumerator CutScene()
        {
            if (YandexGame.savesData.IsEscapeHomeHerobrine)
            {
                _herobrine.SetActive(false);
                yield break;
            }

            yield return null;
            
            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
                _mobileInput.SetActive(true);
            else
                _pcInput.SetActive(true);
            
            GameData.Character.UseArea.gameObject.SetActive(false);
            GameData.Character.transform.position = _startPoint.position;
            GameData.Character.gameObject.SetActive(true);
            
            yield return new WaitUntil(() => Vector3.Magnitude(GameData.Character.transform.position - _startPoint.position) > 2);

            GameData.MusicAudioSource.clip = _funnyMusic;
            GameData.MusicAudioSource.Play();
            
            var normalSprite = _herobrine.GetComponent<SpriteRenderer>().sprite;
            _herobrine.GetComponent<Animator>().enabled = true;

            yield return new WaitForSeconds(1);
            
            _herobrine.GetComponent<Animator>().enabled = false;
            _herobrine.GetComponent<SpriteRenderer>().sprite = _surpriseSprite; // У Херобрина увеличиваются глаза
            GameData.Character.enabled = false;

            _isToFollow = true;

            for (int i = 0; i < 2; i++) // Вертится из стороны в сторону
            {
                var delta = 0f;
            
                while (delta < 1)
                {
                    _herobrine.transform.eulerAngles = new Vector3(0, 360 * delta, 0);
                    //_herobrine.transform.localScale = new Vector3(Mathf.Lerp(1, 1.5f, delta), 1, 1);
                    delta += Time.deltaTime;
                    yield return null;
                }
            }

            _event.Invoke();
            
            GameData.Dialog.SetReplicas(_replicas);
            yield return new WaitUntil(() => !GameData.Dialog.gameObject.activeSelf); // (Огромные глаза) Ты что здесь делаешь?!!
            GameData.Character.enabled = false;
            
            GameData.Dialog.SetReplicas(_replicas_2);
            yield return new WaitUntil(() => !GameData.Dialog.gameObject.activeSelf); // (Огромные глаза) НЕЕЕЕЕТ!
            GameData.Character.enabled = false;
            
            yield return new WaitForSeconds(0.5f); // Ожидание 1 секунду
            GameData.Character.enabled = false;
            
            _herobrine.GetComponent<SpriteRenderer>().sprite = normalSprite; 
            GameData.Dialog.SetReplicas(_replicas_3); // (Обычный) Ну лан
            yield return new WaitUntil(() => !GameData.Dialog.gameObject.activeSelf); // Курица захвати мне МАЙНКРАФТ!!!

            // Иди захватывай уже

            //GameData.MusicAudioSource.Stop();
            GameData.Character.UseArea.gameObject.SetActive(true);
            _event2.Invoke();
        }
    }
}