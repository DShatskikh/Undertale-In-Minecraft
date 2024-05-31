using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private SaveKeyBool _isFirstPlayKey;

        private IEnumerator Start()
        {
            print(GameData.Saver.LoadKey(_isFirstPlayKey));
            
            if (GameData.Saver.LoadKey(_isFirstPlayKey))
            {
                GameData.Volume = 1;
                PlayerPrefs.SetFloat("Volume", GameData.Volume);

                yield return null;
                GameData.Saver.Save(_isFirstPlayKey, false);
            }
        }
    }
}