using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class CommandManager : MonoBehaviour
    {
        public void StartCommands(List<CommandBase> commands)
        {
            StartCoroutine(AwaitExecute(commands));
        }

        public IEnumerator AwaitExecute(List<CommandBase> commands)
        {
            foreach (var command in commands)
            {
                bool isEnd = false;
                UnityAction action = () => isEnd = true;
                command.Execute(action);
                yield return new WaitUntil(() => isEnd);
            }
        }
    }
}