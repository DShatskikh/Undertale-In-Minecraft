using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class LeverManager : MonoBehaviour
    {
        [SerializeField]
        private PairLeverLamp[] _pairs;

        [SerializeField]
        private AudioClip _activateSound;
        
        [SerializeField]
        private AudioClip _solvedSound;

        [SerializeField]
        private UnityEvent Solved;
        
        [SerializeField]
        private UnityEvent NotSolved;

        [SerializeField]
        private string _identifier;
        
        private bool _isSolve;
        
        private void Awake()
        {
            for (int i = 0; i < _pairs.Length; i++)
            {
                var pair = _pairs[i];
                pair.Lever.Used += OnLeverUsed;
                
                var isActivated = YandexGame.savesData.GetInt($"Lever_{_identifier}_{i}") == 1;
                pair.Lever.Init(isActivated);
                pair.Lamp.Activate(isActivated);
            }
        }

        private void Start()
        {
            if (IsSolve())
                Solved.Invoke();
            else
                NotSolved.Invoke();
        }

        private void OnLeverUsed(Lever lever, bool isActive)
        {
            var index = GetIndex(lever);
            _pairs[index].Lamp.Activate(isActive);
            YandexGame.savesData.SetInt($"Lever{index}", isActive ? 1 : 0);
            
            if (IsSolve())
                Solve();
            else
            {
                GameData.EffectAudioSource.clip = _activateSound;
                GameData.EffectAudioSource.Play();

                if (_isSolve)
                    NotSolved.Invoke();
                
                _isSolve = false;
            }
        }

        private void Solve()
        {
            _isSolve = true;
            
            GameData.EffectAudioSource.clip = _solvedSound;
            GameData.EffectAudioSource.Play();
            
            Solved.Invoke();
        }

        private int GetIndex(Lever lever)
        {
            for (int i = 0; i < _pairs.Length; i++)
            {
                if (_pairs[i].Lever == lever)
                    return i;
            }

            return -1;
        }

        private bool IsSolve()
        {
            foreach (var pair in _pairs)
            {
                if (!pair.Lever.IsOn)
                    return false;
            }

            return true;
        }

        [Serializable]
        private struct PairLeverLamp
        {
            public Lever Lever;
            public Lamp Lamp;
        }
    }
}