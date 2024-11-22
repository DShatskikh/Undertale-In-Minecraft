using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "SpeakAct", menuName = "Data/Acts/Speak", order = 71)]
    public class SpeakActConfig : BaseActConfig
    {
        [Header("Success")]
        public LocalizedString SuccessReaction;
        public LocalizedString SuccessSystemMessage;
        
        [Range(-100, 100)]
        public int SuccessProgress;
        
        [Header("Failed")]
        public LocalizedString FailedReaction;
        public LocalizedString FailedSystemMessage;
        
        [Range(-100, 100)]
        public int FailedProgress;

        [Header("Other")]
        public Sprite[] ImageElements;

        public override Sprite GetIcon() =>
            GameData.AssetProvider.SpeakActIcon;
        
        public override int GetProgress() => 
            SuccessProgress > FailedProgress ? SuccessProgress : FailedProgress;

        public override void Use()
        {
            var screen = Instantiate(GameData.AssetProvider.SpeakActScreenPrefab, GameData.Battle.ActScreenContainer);
            screen.Init(this);
        }
    }
}