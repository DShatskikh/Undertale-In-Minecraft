using UnityEngine;
using YG;

namespace Game
{
    public class BranchActionPalesos : BranchActionBase
    {
        [SerializeField]
        private int _value;

        public override bool IsTrue()
        {
            return YandexGame.savesData.Palesos == _value;
        }
    }
}