using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "DanceAct", menuName = "Data/Acts/Dance", order = 73)]
    public class DanceActConfig : BaseActConfig
    {
        [Range(-7, 7)]
        public int SuccessProgress;
        
        [Range(-7, 7)]
        public int FailedProgress;
        
        public override Sprite GetIcon() => 
            GameData.AssetProvider.DanceActIcon;

        public override int GetProgress() => 
            SuccessProgress > FailedProgress ? SuccessProgress : FailedProgress;

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}