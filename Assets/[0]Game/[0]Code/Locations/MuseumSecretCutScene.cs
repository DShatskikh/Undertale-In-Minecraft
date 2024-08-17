using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MuseumSecretCutScene : MonoBehaviour
    {
        [SerializeField]
        private Transform _exhibitPoint;

        [SerializeField]
        private Transform[] _points;

        [SerializeField]
        private UnityEvent _event;
        
        public void StartCutscene()
        {
            GameData.CharacterController.enabled = false;
            
            var speed = 0.1f;
            
            var commands = new List<CommandBase>()
            {
                new MoveToPointCommand(_exhibitPoint, _points[0].position, speed),
                new UnityEventCommand(_event),
            };
            
            GameData.CommandManager.StartCommands(commands);
        }

        public void Load()
        {
            _exhibitPoint.position = _points[0].position;
        }
    }
}