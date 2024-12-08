using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Game
{
    public class Bench : MonoBehaviour, IUseName
    {
        [SerializeField]
        private LocalizedString _sitString;
        
        [SerializeField]
        private LocalizedString _middleSitString;

        [SerializeField]
        private LocalizedString _endSitString;
        
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private GameObject _hint;

        [SerializeField]
        private LocalizedString _nameButton;
        
        private Coroutine _coroutine;
        private Sequence _sequence;
        private Vector3 _startPosition;

        public LocalizedString Name => _nameButton;
        
        private void OnDisable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _sequence?.Kill();
        }

        private void Sit() => 
            StartCoroutine(AwaitSit());

        private IEnumerator AwaitSit()
        {
            var character = GameData.CharacterController;
            character.GetComponent<Collider2D>().enabled = false;
            character.enabled = false;
            character.View.Flip(true);
            _startPosition = character.transform.position;

            _sequence = DOTween.Sequence();
            yield return _sequence.Append(character.transform.DOMove(_target.position, 1f)).WaitForCompletion();
            character.enabled = false;

            character.View.Sit();
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            yield return new WaitForSeconds(0.5f);
            PixelCrushers.DialogueSystem.Sequencer.Message("\"BenchSit\"");
        }
        
        private void Sit2()
        {
           // StartCoroutine(AwaitSit2());
        }

        //private IEnumerator AwaitSit2()
        //{

       // }

        private void StandUp() => 
            StartCoroutine(AwaitStandUp());

        private IEnumerator AwaitStandUp()
        {
            var character = GameData.CharacterController;
            character.enabled = false;

            _hint.SetActive(true);
            yield return DialogueSystemExtensions.AwaitSubmitEvent();
            //yield return null;
            _hint.SetActive(false);
            
            GameData.CharacterController.View.Reset();
            yield return _sequence.Append(character.transform.DOMove(_startPosition, 1f)).WaitForCompletion();
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);
            GameData.CharacterController.GetComponent<Collider2D>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            PixelCrushers.DialogueSystem.Sequencer.Message("\"BenchEndSit\"");
        }
    }
}