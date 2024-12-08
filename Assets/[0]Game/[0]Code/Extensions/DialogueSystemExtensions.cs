using System.Collections;
using UnityEngine;

namespace Game
{
    public static class DialogueSystemExtensions
    {
        public static IEnumerator AwaitDialogueEvent(string index)
        {
            var isActivation = false;

            void Process(string currentIndex)
            {
                if (currentIndex != index)
                    return;
                    
                isActivation = true;
            }
            
            EventBus.DialogueEvent = Process;
            yield return new WaitUntil(() => isActivation);
            EventBus.DialogueEvent = null;
        }
        
        public static IEnumerator AwaitEndDialogue()
        {
            var isClose = false;
            void Close() => isClose = true;

            EventBus.CloseDialog += Close;
            yield return new WaitUntil(() => isClose);
            EventBus.CloseDialog -= null;
        }
        
        public static IEnumerator AwaitSubmitEvent()
        {
            var isClose = false;
            void Close() => isClose = true;

            EventBus.SubmitUp += Close;
            yield return new WaitUntil(() => isClose);
            EventBus.SubmitUp -= null;
        }
    }
}