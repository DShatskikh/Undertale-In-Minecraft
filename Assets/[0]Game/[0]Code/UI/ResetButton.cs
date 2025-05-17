using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class ResetButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            GameData.Saver.Reset();
            GameData.Saver.Load();
            SceneManager.LoadScene(1);

            GameData.Startup.StartCoroutine(AwaitReset());
        }

        private IEnumerator AwaitReset()
        {
            yield return null;
            
            foreach (var saver in FindObjectsByType<SaveLoadBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                saver.Reset();
                print(saver.name);
            }
            
            SceneManager.LoadScene(1);
        }
    }
}