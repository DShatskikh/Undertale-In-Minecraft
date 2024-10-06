using UnityEngine.SceneManagement;

namespace Game
{
    public class InMenuAnimatedButtonOld : AnimatedButtonOld
    {
        public override void OnClick()
        {
            GameData.Saver.Save();
            SceneManager.LoadScene(0);
        }
    }
}