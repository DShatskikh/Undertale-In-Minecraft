using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "StepSoundPairsConfig", menuName = "Data/StepSoundPairsConfig", order = 82)]
    public class StepSoundPairsConfig : ScriptableObject
    {
        public StepSoundPair StoneStepPair;
        public StepSoundPair GrassStepPair;
        public StepSoundPair WoodStepPair;
        public StepSoundPair DirtStepPair;
        public StepSoundPair SpongeStepPair;
        public StepSoundPair SandStepPair;
    }
}