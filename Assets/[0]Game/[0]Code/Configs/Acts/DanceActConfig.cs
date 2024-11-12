using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "DanceAct", menuName = "Data/Acts/Dance", order = 73)]
    public class DanceActConfig : BaseActConfig
    {
        [Range(-7, 7)]
        public int SuccessProgress;
        
        [Range(-7, 7)]
        public int FailedProgress;

        public LocalizedString SuccessReaction;
        public LocalizedString FailedReaction;
        public Arrow[] Arrows;

        public override Sprite GetIcon() => 
            GameData.AssetProvider.DanceActIcon;

        public override int GetProgress() => 
            SuccessProgress > FailedProgress ? SuccessProgress : FailedProgress;

        public override void Use()
        {
            var screen = Instantiate(GameData.AssetProvider.DanceActScreenPrefab, GameData.Battle.ActScreenContainer);
            screen.Init(this);
        }
    }
    
    public enum Arrow
    {
        Up,
        Down,
        Right,
        Left
    }
}