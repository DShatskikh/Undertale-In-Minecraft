using UnityEngine;

namespace Game
{
    public class ExitGameButton : BaseButton
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}