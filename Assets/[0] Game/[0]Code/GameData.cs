using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game
{
    public static class GameData
    {
        public static Character Character;
        public static Heart Heart { get; set; }
        public static EnemyData EnemyData { get; set; }
        public static Transform CharacterPoint { get; set; }
        public static Transform EnemyPoint { get; set; }
        public static Battle Battle { get; set; }
        public static Dialog Dialog { get; set; }
        public static Button UseButton { get; set; }
        public static Joystick Joystick { get; set; }
        public static bool IsSpeakHerobrine { get; set; }
        public static bool IsCapturedWorld { get; set; }
        public static bool IsNotCapturedWorld { get; set; }
        public static bool IsHat { get; set; }
        public static bool IsGoodEnd { get; set; }
        public static bool IsBadEnd { get; set; }
        public static bool IsStrangeEnd { get; set; }
        public static bool IsNotIntroduction { get; set; }
        public static float Volume { get; set; }
        public static AudioMixerGroup Mixer { get; set; }
        public static Button ToMenuButton { get; set; }
        public static Startup Startup { get; set; }

        public static IEnumerable<Location> Locations;

        public static int Palesos;

        public static Monolog Monolog;
        public static Select Select;
        public static AudioSource EffectAudioSource;
        public static AudioSource MusicAudioSource;
        public static int LocationIndex;
        public static int BattleProgress;
        public static CinemachineConfiner2D CinemachineConfiner;
        public static GameObject Arena;
        public static GameObject Introduction;
        public static GameObject Menu;
        public static TMP_Text SaveText;
        public static GameObject InputCanvas;
        public static GameObject GameOver;

        public static Saver Saver;

        public static int MaxHealth;
        public static int Health;
        public static bool IsPrisonKey;
        public static bool IsDeveloperKey;
        public static bool IsGoldKey;
        public static bool IsCheat;
        public static bool IsNotFirstPlay;
        public static TimerBeforeAdsYG TimerBeforeAdsYG;
    }
}