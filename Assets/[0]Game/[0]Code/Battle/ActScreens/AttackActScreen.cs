using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class AttackActScreen : UIPanelBase
    {
        [SerializeField]
        private Image _target;

        [SerializeField]
        private TMP_Text[] _texts;
        
        [SerializeField]
        private Strip _strip1, _strip2;

        [SerializeField]
        private Transform _cross;

        [SerializeField]
        private Transform _divider;
        
        private Strip _activeStrip;
        private Coroutine _coroutine;
        private AttackActConfig _config;

        public void Init(AttackActConfig attackActConfig)
        {
            _config = attackActConfig;
            Activate(true);
            StartCoroutine(AwaitActive());
        }

        private IEnumerator AwaitActive()
        {
            _target.color = _target.color.SetA(0);
            _strip1.gameObject.SetActive(false);
            _strip2.gameObject.SetActive(false);
            _cross.gameObject.SetActive(false);

            foreach (var text in _texts)
            {
                text.color = text.color.SetA(0);
                
                var changeAlphaTextCommand = new ChangeAlphaTextCommand(text, 1, 0.5f);
                StartCoroutine(changeAlphaTextCommand.Await());
            }
            
            var changeAlphaCommand = new ChangeAlphaImageCommand(_target, 1, 0.5f);
            yield return changeAlphaCommand.Await();

            _activeStrip = _strip1;
            _strip1.StartMove();
        }

        private IEnumerator AwaitStopStrip()
        {
            var stip = _activeStrip;
            _activeStrip = null;
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            yield return stip.AwaitStop();

            if (stip == _strip1)
            {
                _activeStrip = _strip2;
                _strip2.StartMove();
            }
            else
            {
                StartCoroutine(AwaitFinallyAnimation());
            }
        }

        private IEnumerator AwaitFinallyAnimation()
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
            _cross.gameObject.SetActive(true);
            _cross.position = new Vector3(_strip1.GetPositionView.x, _strip2.GetPositionView.y);

            yield return new WaitForSeconds(0.5f);
            
            _strip1.gameObject.SetActive(false);
            _strip2.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(0.25f);
            
            _cross.gameObject.SetActive(false);

            foreach (var text in _texts)
            {
                text.color = text.color.SetA(0);
                
                var changeAlphaTextCommand = new ChangeAlphaTextCommand(text, 0, 0.5f);
                StartCoroutine(changeAlphaTextCommand.Await());
            }
            
            var changeAlphaCommand = new ChangeAlphaImageCommand(_target, 0, 0.5f);
            yield return changeAlphaCommand.Await();
            
            yield return new WaitForSeconds(0.5f);

            var maxDistance = Vector3.Distance(_divider.position, _target.transform.position);
            var distance = Vector3.Distance(_cross.position, _target.transform.position);
            
            var damage = Mathf.Lerp(3, 8, 1 - distance / maxDistance);
            
            yield return GameData.Battle.SessionData.BattleController.AwaitActReaction("Attack", damage);
        }

        public void EndMove()
        {
            if (_activeStrip)
            {
                if (_coroutine != null)
                    StopCoroutine(_coroutine);
                
                _coroutine = StartCoroutine(AwaitStopStrip());
            }
        }
        
        public override void OnSubmitDown()
        {
            
        }

        public override void OnSubmitUp()
        {
            EndMove();
        }

        public override void OnCancel()
        {
            
        }
    }
}