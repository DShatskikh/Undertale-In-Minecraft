// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.DeviceSimulation;
#endif

namespace RimuruDev
{
    [Serializable]
    public enum CurrentDeviceType : byte
    {
        PC = 0,
        Mobile = 1,
    }

    [SelectionBase]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-100)]
    [HelpURL("https://github.com/RimuruDev/Unity-WEBGL-DeviceTypeDetector")]
    public sealed class DeviceTypeDetector : MonoBehaviour
    {
        [field: SerializeField] public CurrentDeviceType CurrentDeviceType { get; private set; }

#if UNITY_2020_1_OR_NEWER
        [SerializeField] private bool enableDeviceSimulator = true;
#endif
        private void Awake()
        {
            if (IsMobile() && enableDeviceSimulator)
            {
                Debug.Log("WEBGL -> Mobile");
                CurrentDeviceType = CurrentDeviceType.Mobile;
            }
            else
            {
                Debug.Log("WEBGL -> PC");
                CurrentDeviceType = CurrentDeviceType.PC;
            }
        }

        private void Start()
        {
            Lua.RegisterFunction(nameof(IsPlatform), this, SymbolExtensions.GetMethodInfo(() => IsPlatform(string.Empty)));
        }

        private void OnDestroy()
        {
            Lua.UnregisterFunction(nameof(IsPlatform));
        }
        
        private bool IsPlatform(string platformName) => 
            Enum.GetName(typeof(CurrentDeviceType), CurrentDeviceType) == platformName;

#if UNITY_EDITOR
        public static bool IsMobile()
        {
#if UNITY_2020_1_OR_NEWER
            if (DeviceSimulatorExists() && IsDeviceSimulationActive())
                return true;
#endif
            return false;
        }

        private static bool DeviceSimulatorExists()
        {
            var simulatorType = typeof(Editor).Assembly.GetType("UnityEditor.DeviceSimulation.DeviceSimulator");
            return simulatorType != null;
        }

        private static bool IsDeviceSimulationActive()
        {
            var simulatorType = typeof(Editor).Assembly.GetType("UnityEditor.DeviceSimulation.DeviceSimulator");
            if (simulatorType != null)
            {
                var simulatorInstance = simulatorType.GetProperty("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)?.GetValue(null);
                var isDeviceActive = simulatorType.GetProperty("isDeviceActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(simulatorInstance);
                return (bool)isDeviceActive;
            }
            return false;
        }
#else
        [System.Runtime.InteropServices.DllImport("__Internal")]
        public static extern bool IsMobile();
#endif
    }
}