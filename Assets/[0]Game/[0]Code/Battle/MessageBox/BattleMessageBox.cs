using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class BattleMessageBox : MonoBehaviour
    {
        private Coroutine _coroutine;
        private BattleMessageBoxView _view;
        private int _countSymbol;
        private int _index;
        private string _result;
        private UnityAction _action;
        private LocalizedString[] _messages;

        public void Show(LocalizedString[] messages, UnityAction action)
        {
            if (!_view)
                _view = GetComponent<BattleMessageBoxView>();
            
            gameObject.SetActive(true);
            _index = 0;
            _action = action;
            _messages = messages;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypeText(messages[0]));
            
            EventBus.SubmitUp = OnSubmit;
            EventBus.CancelUp = OnCancel;
        }

        private void OnCancel()
        {
            print("Cancel");
            
            if (_result != "" && _countSymbol != _result.Length)
                AllShowText();
        }

        private void OnSubmit()
        {
            if (_result == "")
                return;
            
            if (_countSymbol < _result.Length)
                AllShowText();
            else
            {
                _index++;
                
                if (_index < _messages.Length)
                {
                    if (_coroutine != null)
                        StopCoroutine(_coroutine);
                    
                    _coroutine = StartCoroutine(TypeText(_messages[_index]));
                }
                else
                {
                    Close();   
                }
            }
        }

        private void AllShowText()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _countSymbol = _result.Length;
            _view.SetText(_result);
        }

        private void Close()
        {
            EventBus.SubmitUp = null;
            EventBus.CancelUp = null;

            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _action.Invoke();
        }

        private IEnumerator TypeText(LocalizedString message)
        {
            _countSymbol = 0;
            var text = "";
            _result = "";
            _view.SetText("");
            //_view.SetShake(message.Shaking);
            
            var messageOperation = message.GetLocalizedStringAsync();
            
            while (!messageOperation.IsDone)
                yield return null;

            _result = messageOperation.Result;
                
            while (_countSymbol < _result.Length)
            {
                if (_result[_countSymbol] == '<')
                {
                    while (_result[_countSymbol] != '>')
                    {
                        text += _result[_countSymbol];
                        _countSymbol++;
                    }
                }
                
                text += _result[_countSymbol];
                _view.SetText(text);
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }
    }
}