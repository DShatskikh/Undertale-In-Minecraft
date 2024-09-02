using UnityEngine.SceneManagement;

namespace Game
{
    public class InMenuAnimatedButton : AnimatedButton
    {
        public override void OnClick()
        {
            GameData.Saver.Save();
            SceneManager.LoadScene(0);
        }
    }
}