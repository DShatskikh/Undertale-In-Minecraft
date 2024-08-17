using UnityEngine;
using YG;

namespace Game
{
    public class AddCheat : MonoBehaviour
    {
        public void Use()
        {
            YandexGame.savesData.IsCheat = true;
            GameData.Character.HatPoint.MaskShowAndHide(true);
        }
    }
}