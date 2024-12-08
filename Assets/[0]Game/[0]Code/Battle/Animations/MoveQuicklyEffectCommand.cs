using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MoveQuicklyEffectCommand : AwaitCommand
    {
        private readonly Sprite _sprite;
        private readonly Vector2 _startPoint;
        private readonly Vector2 _endPoint;
        private readonly float _duration;
        private readonly Transform _container;
        private readonly List<GameObject> _gameObjects = new();
        private readonly bool _isFlip;

        public MoveQuicklyEffectCommand(Sprite sprite, Vector2 startPoint, Vector2 endPoint, float duration,
            Transform container, bool isFlip)
        {
            _sprite = sprite;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _duration = duration;
            _container = container;
            _isFlip = isFlip;
        }

        public override void Execute(UnityAction action)
        {
            GameData.CoroutineRunner.StartCoroutine(AwaitExecute(action));
        }

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            var count = 12;
            
            // Вычисляем вектор направления и расстояние между точками
            Vector3 direction = (_endPoint - _startPoint).normalized;
            float distance = Vector3.Distance(_startPoint, _endPoint);
            float spacing = distance / (count + 1); // Расстояние между объектами

            for (int i = 1; i <= count - 1; i++)
            {
                // Вычисляем позицию каждого объекта
                Vector3 position = (Vector3)_startPoint + direction * (spacing * i);
                var gameObject = Object.Instantiate(new GameObject(), position, Quaternion.identity, _container); // Создаем объект на позиции
                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = _sprite;
                spriteRenderer.sortingOrder = 10;
                spriteRenderer.flipX = _isFlip;
                var changeAlphaCommand = new ChangeAlphaCommand(spriteRenderer, 0, 0.25f);
                changeAlphaCommand.Execute(null);
                _gameObjects.Add(gameObject);
                
                yield return new WaitForSeconds(_duration / count);
            }

            //yield return new WaitForSeconds(0.25f);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                Object.Destroy(_gameObjects[i]);
            }

            action?.Invoke();
        }
    }
}