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

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private SaveKeyBool _isFirstPlayKey;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void Start()
        {
            if (GameData.Saver.LoadKey(_isFirstPlayKey))
                _label.text = "Новая игра";
            else
                _label.text = "Продолжить";
        }

        private void OnClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}