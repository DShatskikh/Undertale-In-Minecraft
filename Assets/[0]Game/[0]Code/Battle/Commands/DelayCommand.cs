using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DelayCommand : CommandBase
    {
        private float _delay;
        
        public DelayCommand(float delay)
        {
            _delay = delay;
        }
        
        public override void Execute(UnityAction action)
        {
            Debug.Log("DelayCommand");
            GameData.Startup.StartCoroutine(Await(action));
        }

        private IEnumerator Await(UnityAction action)
        {
            yield return new WaitForSeconds(_delay);
            action?.Invoke();
        }
    }
}