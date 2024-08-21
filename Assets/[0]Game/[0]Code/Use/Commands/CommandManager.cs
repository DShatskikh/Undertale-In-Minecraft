using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class CommandManager : MonoBehaviour
    {
        private CommandBase _currentCommand;
        private Coroutine _coroutine;
        public CommandBase CurrentCommand => _currentCommand;
        
        public void StartCommands(List<CommandBase> commands)
        {
            _coroutine = StartCoroutine(AwaitExecute(commands));
        }

        private IEnumerator AwaitExecute(List<CommandBase> commands)
        {
            foreach (var command in commands)
            {
                bool isEnd = false;
                UnityAction action = () => isEnd = true;
                command.Execute(action);
                yield return new WaitUntil(() => isEnd);
            }
        }

        public void StopExecute()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}