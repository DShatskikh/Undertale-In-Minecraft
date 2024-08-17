using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            StartCoroutine(Init());
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }


        private IEnumerator Init()
        {
            yield return null;
        }
        
        private void OnClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}