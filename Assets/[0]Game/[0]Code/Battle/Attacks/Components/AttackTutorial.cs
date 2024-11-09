using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class AttackTutorial : AttackBase
    {
        [SerializeField]
        private TMP_Text _text;

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            foreach (var message in Messages)
            {
                var localizedString = message.LocalizedString;
                var messageOperation = localizedString.GetLocalizedStringAsync();
            
                while (!messageOperation.IsDone)
                    yield return null;

                var result = messageOperation.Result;
                
                _text.text = result;
                var isSubmit = false; 
                EventBus.Submit = () => isSubmit = true;
                yield return new WaitUntil(() => isSubmit);
                EventBus.Submit = null;
            }
            
            YandexGame.savesData.IsTutorialComplited = true;
            action.Invoke();
        }
    }
}