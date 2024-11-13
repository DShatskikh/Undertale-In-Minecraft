using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject _container;

        [SerializeField]
        private Button _submitButton, _cancelButton;
        
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        private IEnumerator Start()
        {
            _submitButton.onClick.AddListener(OnSubmitButtonClicked);
            _cancelButton.onClick.AddListener(OnCancelButtonClicked);
 
            _canvasGroup.alpha = 0;
            
            yield return new WaitForSeconds(1.5f);

            var progress = 0f;

            while (progress < 1f / 1.5f)
            {
                yield return null;
                progress += Time.deltaTime;
                _canvasGroup.alpha = progress;
            }
        }

        private void OnSubmitButtonClicked()
        {
            SceneManager.LoadScene(1);
        }

        private void OnCancelButtonClicked()
        {
            YandexGame.RewVideoShow(3);
        }
    }
}