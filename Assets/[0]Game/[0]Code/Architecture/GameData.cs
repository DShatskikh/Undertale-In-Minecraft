using Cinemachine;
using MoreMountains.Feedbacks;
using RimuruDev;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public static class GameData
    {
        public static CharacterController CharacterController;
        public static HeartController HeartController { get; set; }
        public static EnemyData EnemyData { get; set; }
        public static Transform CharacterPoint { get; set; }
        public static Transform EnemyPoint { get; set; }
        public static Battle Battle { get; set; }
        public static DialogViewModel Dialog { get; set; }
        public static Button UseButton { get; set; }
        public static Joystick Joystick { get; set; }
        public static AudioMixer AudioMixer;
        public static Startup Startup { get; set; }
        public static End CurrentEnd { get; set; }
        public static CurrentDeviceType DeviceType { get; set; }
        public static bool IsLoad { get; set; }
        public static AssetProvider AssetProvider { get; set; }
        public static bool IsCanStartBattle { get; set; }
        public static CinemachineVirtualCamera CinemachineVirtualCamera { get; set; }
        public static SaverTimer SaverTimer { get; set; }
        public static InputManager InputManager { get; set; }
        public static AdsManager AdsManager { get; set; }
        public static PlayerInput PlayerInput { get; set; }
        public static MonoBehaviour CoroutineRunner { get; set; }

        public static LocationsManager LocationsManager;
        public static MonologViewModel Monolog;
        public static SelectViewModel Select;
        public static EffectSoundPlayer EffectSoundPlayer;
        public static MusicPlayer MusicPlayer;
        public static int BattleProgress;
        public static CinemachineConfiner2D CinemachineConfiner;
        public static GameObject Introduction;
        public static GameObject Menu;
        public static TMP_Text SaveText;
        public static GameObject GameOver;
        
        public static Saver Saver;
        public static CommandManager CommandManager;
        public static CompanionsManager CompanionsManager;
        public static EndingsManager EndingsManager;
        public static MMF_Player ImpulseMMFPlayer;
    }
}