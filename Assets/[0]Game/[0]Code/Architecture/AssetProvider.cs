using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "AssetProvider", menuName = "Data/AssetProvider", order = 70)]
    public class AssetProvider : ScriptableObject
    {
        [Header("Sounds")]
        public AudioClip LeverSound;
        public AudioClip ClickSound;
        public AudioClip SelectSound;
        public AudioClip DoorSound;
        public AudioClip JumpSound;
        public AudioClip PistonSound;
        public AudioClip HypnosisSound;
        public AudioClip BombSound;
        public AudioClip DamageSound;
        public AudioClip SpareSound;
        public AudioClip HurtSound;
        public AudioClip GrazeSound;
        public AudioClip PhoneSound;
        public AudioClip WarningSound;
        
        [Header("Configs")]
        public StepSoundPairsConfig StepSoundPairsConfig;
        public TileTagConfig TileTagConfig;
        public GameplayMenuConfig[] GameplayMenuConfigs;
        public ExitSlotConfig[] ExitSlotConfigs;
        public MenuSlotConfig[] MenuSlotConfigs;
        public ItemsConfigContainer ItemsConfigContainer;
        public EndingConfig[] EndingsConfigs;
        
        [Header("Prefabs")]
        public ActSlotController ActSlotPrefab;
        public TestButton TestButton;
        public GameplayMenuSlotViewModel GameplayMenuSlotPrefab;
        public InventorySlotViewModel InventorySlotPrefab;
        public ExitSlotViewModel ExitSlotPrefab;
        public MenuSlotViewModel MenuSlotPrefab;
        public GuideSlotViewModel GuideSlotPrefab;
        public PuzzleLeverSlotView PuzzleLeverSlotPrefab;
        public PuzzleButtonSlotView PuzzleButtonSlotPrefab;
        public PuzzleArrowSlotView PuzzleArrowSlotPrefab;
        public SpeakActScreen SpeakActScreenPrefab;
        public SpeakAtcSlot SpeakAtcSlotPrefab;
        public AttackActScreen AttackActScreenPrefab;
        public DanceActScreen DanceActScreenPrefab;
        
        [Header("Iocns")]
        public Sprite CharacterIcon;
        public Sprite SpeakActIcon;
        public Sprite DanceActIcon;
        public Sprite AttackActIcon;
        public Sprite SightActIcon;

        [Header("Other")]
        public Color SelectColor;
        public Color DeselectColor;
    }
}