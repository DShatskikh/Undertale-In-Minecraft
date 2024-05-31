using UnityEngine;

namespace Game
{
    public class OpenMenuUpdater : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("OpenMenu"))
            {
                GameData.GameMenu.gameObject.SetActive(!GameData.GameMenu.gameObject.activeSelf);
            }
        }
    }
}