using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class PuzzleEnderCrystalsManager : MonoBehaviour
    {
        [SerializeField]
        private PuzzleEnderCrystal[] _puzzleEnderCrystals;

        [SerializeField]
        private GameObject _endRay;

        [SerializeField]
        private GameObject _explosions;
        
        [SerializeField]
        private GameObject _enderCrystals;

        [SerializeField]
        private MMF_Player _mmfPlayer;

        [SerializeField]
        private FakeHeroCutscene_3 _cutscene3;
        
        private void Start()
        {
            foreach (var crystal in _puzzleEnderCrystals)
            {
                crystal.OnUse = TryDecision;
            }
        }

        private void TryDecision()
        {
            foreach (var crystal in _puzzleEnderCrystals)
            {
                if (crystal.enabled)
                    return;
            }

            StartCoroutine(AwaitDecision());
        }

        private IEnumerator AwaitDecision()
        {
            GameData.CharacterController.enabled = false;
            
            _endRay.SetActive(true);

            yield return new WaitForSeconds(3);
            
            _endRay.SetActive(false);
            _enderCrystals.SetActive(false);
            _explosions.SetActive(true);

            yield return new WaitForSeconds(0.7f);
            
            _explosions.SetActive(false);
            
            yield return new WaitForSeconds(1);

            yield return _mmfPlayer.PlayFeedbacksCoroutine(Vector3.zero);
            
            GameData.CharacterController.enabled = true;
            _cutscene3.StartCutscene();
        }
    }
}