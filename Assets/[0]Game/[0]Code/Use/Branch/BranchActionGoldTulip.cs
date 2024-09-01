using UnityEngine;
using YG;

namespace Game
{
    public class BranchActionGoldTulip : BranchActionBase
    {
        [SerializeField]
        private int _value;
        
        public override bool IsTrue()
        {
            return YandexGame.savesData.GoldTulip == _value;
        }
    }
}