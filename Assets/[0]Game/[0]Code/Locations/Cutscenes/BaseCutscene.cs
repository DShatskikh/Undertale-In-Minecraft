using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public abstract class BaseCutscene : MonoBehaviour
    {
        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;
        
        protected abstract IEnumerator AwaitCutscene();
        
        public void StartCutscene()
        {
            StartCoroutine(AwaitCutscene());
        }
        
        protected IEnumerator AwaitDialog()
        {
            _dialogueSystemTrigger.OnUse();
            var isEndDialogue = false;
            EventBus.CloseDialog = () => isEndDialogue = true;
            yield return new WaitUntil(() => isEndDialogue);
            GameData.CharacterController.enabled = false;
        }
    }
}