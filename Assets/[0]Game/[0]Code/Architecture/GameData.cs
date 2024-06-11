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
        public static float Volume { get; set; }
        public static AudioMixerGroup Mixer { get; set; }
        public static Startup Startup { get; set; }
        public static MonoBehaviour CoroutineRunner { get; set; }
        public static int Palesos { get; set; }
        public static AudioSource TextAudioSource { get; set; }
        public static AudioClip ClickSound { get; set; }
        public static string Version { get; set; }
        public static GameMenu GameMenu { get; set; }
        public static SubmitUpdater SubmitUpdater { get; set; }
        public static CancelUpdater CancelUpdater { get; set; }
        public static OpenMenuUpdater OpenMenuUpdater { get; set; }
        public static SaveKeyInt MoneyKey { get; set; }
        public static MoneyLabel MoneyLabel { get; set; }

        public static IEnumerable<Location> Locations;
        
        public static Monolog Monolog;
        public static Select Select;
        public static AudioSource EffectAudioSource;
        public static AudioSource MusicAudioSource;
        public static int LocationIndex;
        public static int BattleProgress;
        public static GameObject Arena;
        public static GameObject GameOver;

        public static Saver Saver;
        public static GameObject Arrow;

        public static CinemachineConfiner2D CinemachineConfiner;
    }
}