using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using YG;

namespace Game
{
    public class BranchActionCheckKey : BranchActionBase
    {
        public override bool IsTrue()
        {
            return YandexGame.savesData.IsPrisonKey;
        }
    }
}