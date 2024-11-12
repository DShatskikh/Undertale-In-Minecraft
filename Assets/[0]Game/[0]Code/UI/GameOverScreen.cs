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
        
        private IEnumerator Start()
        {
            _submitButton.onClick.AddListener(OnSubmitButtonClicked);
            _cancelButton.onClick.AddListener(OnCancelButtonClicked);
            
            _container.SetActive(false);
            yield return new WaitForSeconds(2f);
            _container.SetActive(true);
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