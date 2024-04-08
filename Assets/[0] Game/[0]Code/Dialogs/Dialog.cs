using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] 
        private DialogView _view;

        [SerializeField] 
        private UIDocument _ui;
        
        [SerializeField]
        private AudioSource _audioSource;
        
        [SerializeField] 
        private AudioClip _clickSound;
        
        private Replica[] _replicas;
        private int _indexReplica;
        private Coroutine _coroutine;

        public void SetReplicas(Replica[] replicas)
        {
            gameObject.SetActive(true);
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            var button = _ui.rootVisualElement.Q<Button>("Next_button");
            button.clicked += Next;
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
            
            GameData.EffectAudioSource.clip = _clickSound;
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
            var text = _replicas[_indexReplica].Text;
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
                _audioSource.Play();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }

        private void UpdateView()
        {
            var replica = _replicas[_indexReplica];
            _view.SetText(replica.Text);
            _view.SetIcon(replica.Icon);
        }

        private void Close()
        {
            EventBus.OnSubmit = null;
            GameData.Character.enabled = true;
            gameObject.SetActive(false);
            EventBus.OnCloseDialog?.Invoke();
            EventBus.OnCloseDialog = null;
        }
    }
}