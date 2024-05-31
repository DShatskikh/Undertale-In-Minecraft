using System.Collections;
using UnityEngine;

namespace Game
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] 
        private DialogView _view;

        private Replica[] _replicas;
        private int _indexReplica;
        private Coroutine _coroutine;
        private bool _isNotBackCharacter;

        public void Show(Replica[] replicas)
        {
            Show(replicas, false);
        }

        public void Show(Replica[] replicas, bool isNotBackCharacter)
        {
            gameObject.SetActive(true);
            _isNotBackCharacter = isNotBackCharacter;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            EventBus.OnSubmit += Next;
            GameData.Character.enabled = false;
            _replicas = replicas;
            _indexReplica = 0;
            UpdateView();
            Next();
        }

        public void Next()
        {
            if (!gameObject.activeSelf)
                Debug.LogError("Ошибка");
            
            GameData.EffectAudioSource.clip = GameData.ClickSound;
            GameData.EffectAudioSource.Play();
            
            if (_indexReplica >= _replicas.Length)
            {
                Close();
                return;
            }

            UpdateView();
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypeText());
            _indexReplica++;
        }

        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            var text = _replicas[_indexReplica].Text.GetLocalizedString();
            string currentText = "";

            while (_countSymbol != text.Length)
            {
                if (text[_countSymbol] == '<')
                {
                    while (text[_countSymbol] != '>')
                    {
                        currentText += text[_countSymbol];
                        _countSymbol++;
                    }
                }

                currentText += text[_countSymbol];
                _view.SetText(currentText);
                GameData.TextAudioSource.Play();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }

        private void UpdateView()
        {
            var replica = _replicas[_indexReplica];
            _view.SetText(replica.Text.GetLocalizedString());
            _view.SetIcon(replica.Icon);
        }

        private void Close()
        {
            EventBus.OnSubmit = null;
            
            if (!_isNotBackCharacter)
                GameData.Character.enabled = true;
            
            gameObject.SetActive(false);
            EventBus.OnCloseDialog?.Invoke();
            EventBus.OnCloseDialog = null;
        }
    }
}