using System.Linq;
using UnityEngine;
using YG;

namespace Game
{
    public class BranchActionCompanion : BranchActionBase
    {
        [SerializeField]
        private string _companionName;
        
        public override bool IsTrue()
        {
            return YandexGame.savesData.Companions.Any(companion => _companionName == companion);
        }
    }
}