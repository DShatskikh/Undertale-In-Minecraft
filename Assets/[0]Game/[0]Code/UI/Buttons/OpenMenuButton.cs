using UnityEngine.SceneManagement;

namespace Game
{
    public class OpenMenuButton : AnimatedButtonOld
    {
        public override void OnClick()
        {
            GameData.Saver.Save();
            SceneManager.LoadScene(0);
        }
    }
}