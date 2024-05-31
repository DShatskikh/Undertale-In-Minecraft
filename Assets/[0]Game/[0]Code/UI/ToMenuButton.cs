using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ToMenuButton : BaseButton
    {
        protected override void OnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}