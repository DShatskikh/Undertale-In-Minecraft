using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SkipIntroCommand : CommandBase
    {
        private readonly Transform[] _points;

        public SkipIntroCommand(Transform[] points)
        {
            _points = points;
        }

        public override void Execute(UnityAction action)
        {
            var characterTransform = GameData.CharacterController.transform;
            var enemyTransform = GameData.EnemyData.Enemy.transform;
            
            characterTransform.position = GameData.CharacterPoint.position;
            enemyTransform.position = GameData.EnemyPoint.position;

            var companions = GameData.CompanionsManager.GetAllCompanions;
            
            for (int i = 0; i < companions.Count; i++) 
                companions[i].transform.position = _points[i].position;

            GameData.HeartController.enabled = true;
            action?.Invoke();
        }
    }
}