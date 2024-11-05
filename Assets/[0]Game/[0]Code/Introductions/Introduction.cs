using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class Introduction : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform[] _slides;
        
        [SerializeField]
        private Button _continueButton, _closeButton;

        [SerializeField]
        private RectTransform _centerPoint;
        
        [SerializeField]
        private Replica[] _replicas;

        [SerializeField]
        private UnityEvent _unityEvent;
        
        private int _index;

        private void Start()
        {
            _continueButton.gameObject.SetActive(false);
            _closeButton.gameObject.SetActive(false);
            
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);

            _slides[_index].gameObject.SetActive(false);
            GameData.LocationsManager.gameObject.SetActive(false);
            GameData.CharacterController.gameObject.SetActive(false);
            _closeButton.gameObject.SetActive(YandexGame.savesData.IsOneOrMoreEnd);
            GameData.InputManager.Hide();
            
            StartCoroutine(AwaitShow());
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnContinueButtonClicked()
        {
            StartCoroutine(_index + 1 >= _slides.Length ? AwaitClose() : AwaitNextSlide());
        }
        
        private void OnCloseButtonClicked()
        {
            StartCoroutine(AwaitClose());
        }

        private IEnumerator AwaitClose()
        {
            yield return AwaitHide();
            yield return new DialogCommand(_replicas, null, null).Await();
            
            GameData.InputManager.Show();
            GameData.LocationsManager.gameObject.SetActive(true);
            GameData.LocationsManager.SwitchLocation("HerobrineHome", 0);
            GameData.CharacterController.gameObject.SetActive(true);
            _unityEvent.Invoke();
        }

        private IEnumerator AwaitShow()
        {
            yield return null;

            _slides[_index].gameObject.SetActive(true);
            _slides[_index].position = _centerPoint.position.AddX(25);
            yield return new MoveToPointCommand(_slides[_index], _centerPoint.position, 1).Await();
            
            _continueButton.gameObject.SetActive(true);
            _closeButton.gameObject.SetActive(true);
        }
        
        private IEnumerator AwaitHide()
        {
            _continueButton.gameObject.SetActive(false);
            _closeButton.gameObject.SetActive(false);
            
            yield return new MoveToPointCommand(_slides[_index], _centerPoint.position.AddX(-25), 1).Await();
            
            _slides[_index].gameObject.SetActive(false);
        }

        private IEnumerator AwaitNextSlide()
        {
            yield return AwaitHide();
            _index++;
            yield return AwaitShow();
        }
    }
}