using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "DanceAct", menuName = "Data/Acts/Dance", order = 73)]
    public class DanceActConfig : BaseActConfig
    {
        [Range(-30, 30)]
        public int SuccessProgress;
        
        [Range(-30, 30)]
        public int FailedProgress;

        public LocalizedString SuccessSystemMessage;
        public LocalizedString SuccessReaction;
        public LocalizedString FailedSystemMessage;
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