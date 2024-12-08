using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class UseButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        
        [SerializeField]
        private LocalizedString _defaultText;
        
        public void SetText(LocalizedString useName)
        {
            StartCoroutine(AwaitLoadText(useName));
        }

        public void ResetText()
        {
            StartCoroutine(AwaitLoadText(_defaultText));
        }

        private IEnumerator AwaitLoadText(LocalizedString localizedString)
        {
            var loadTextCommand = new LoadTextCommand(localizedString);
            yield return loadTextCommand.Await();
            _label.text = loadTextCommand.Result;
        }
    }
}