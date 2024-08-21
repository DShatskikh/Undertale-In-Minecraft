using System.Collections.Generic;
using Cinemachine;
using RimuruDev;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
        public static Dialog Dialog { get; set; }
        public static Button UseButton { get; set; }
        public static Joystick Joystick { get; set; }
        public static AudioMixerGroup Mixer { get; set; }
        public static Button ToMenuButton { get; set; }
        public static Startup Startup { get; set; }
        public static End CurrentEnd { get; set; }
        public static CurrentDeviceType DeviceType { get; set; }
        public static bool IsLoad { get; set; }
        public static VolumeSlider VolumeSlider { get; set; }
        public static AssetProvider AssetProvider { get; set; }
        public static bool IsCanStartBattle { get; set; }

        public static LocationsManager LocationsManager;
        public static Monolog Monolog;
        public static Select Select;
        public static EffectSoundPlayer EffectSoundPlayer;
        public static MusicPlayer MusicPlayer;
        public static int BattleProgress;
        public static CinemachineConfiner2D CinemachineConfiner;
        public static GameObject Introduction;
        public static GameObject Menu;
        public static TMP_Text SaveText;
        public static GameObject InputCanvas;
        public static GameObject GameOver;
        
        public static Saver Saver;
        public static TimerBeforeAdsYG TimerBeforeAdsYG;
        public static CommandManager CommandManager;
        public static CompanionsManager CompanionsManager;
        public static EndingsManager EndingsManager;
    }
}