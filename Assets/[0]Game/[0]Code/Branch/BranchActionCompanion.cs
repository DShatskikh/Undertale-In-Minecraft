using System.Linq;
using UnityEngine;
using YG;

namespace Game
{
    public class BranchActionCompanion : BranchActionBase
    {
        [SerializeField]
        private CompanionType _companion;
        
        public override bool IsTrue()
        {
            return YandexGame.savesData.Companions.Any(companion => _companion == companion);
        }
    }
}