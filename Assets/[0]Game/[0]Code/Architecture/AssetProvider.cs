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
        
        [Header("Configs")]
        public StepSoundPairsConfig StepSoundPairsConfig;
        public TileTagConfig TileTagConfig;
        public GameplayMenuConfig[] GameplayMenuConfigs;
        public ExitSlotConfig[] ExitSlotConfigs;
        public MenuSlotConfig[] MenuSlotConfigs;
        public ItemsConfigContainer ItemsConfigContainer;
        public GuideConfig[] GuideConfigs;
        
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
        public SpeakActScreen SpeakActScreenPrefab;
        public SpeakAtcSlot SpeakAtcSlotPrefab;
        
        [Header("Iocns")]
        public Sprite CharacterIcon;
        public Sprite SpeakActIcon;
        
        [Header("Other")]
        public Color SelectColor;
        public Color DeselectColor;
    }
}