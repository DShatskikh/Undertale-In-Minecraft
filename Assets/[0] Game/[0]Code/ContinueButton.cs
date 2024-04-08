using System.Collections;
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
            /*GameData.Joystick.gameObject.SetActive(false);
            GameData.ToMenuButton.gameObject.SetActive(false);
            
            GameData.TimerBeforeAdsYG.gameObject.SetActive(false);*/
        }
        
        private void OnClick()
        {
            SceneManager.LoadScene(1);

            /*
           GameData.Joystick.gameObject.SetActive(true);
           GameData.Menu.SetActive(false);
           GameData.TimerBeforeAdsYG.gameObject.SetActive(true);

          
           if (GameData.IsHat)
               GameData.Character.HatPoint.Show();
           else
               GameData.Character.HatPoint.Hide();
           
           if (!GameData.IsNotIntroduction)
           {
               GameData.Introduction.SetActive(true);
           }
           else
           {
               GameData.Character.enabled = true;
               GameData.Character.gameObject.SetActive(true);
               GameData.Character.transform.position = GameData.Saver.LoadPosition();
               GameData.Locations.ToArray()[GameData.LocationIndex].gameObject.SetActive(true);
               GameData.ToMenuButton.gameObject.SetActive(true);
           }*/
        }
    }
}