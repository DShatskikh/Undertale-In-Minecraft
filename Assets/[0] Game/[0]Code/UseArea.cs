using System;
using UnityEngine;

namespace Game
{
    public class UseArea : MonoBehaviour
    {
        [SerializeField]
        private float _radius;

        private UseObject _previousUseObject;

        private void OnEnable()
        {
            _previousUseObject = null;
        }

        private void OnDisable()
        {
            if (GameData.UseButton)
                GameData.UseButton.gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

            float minDistance = float.MaxValue;
            UseObject nearestUseObject = null;

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out UseObject useObject))
                {
                    var currentDistance = Vector2.Distance(transform.position, useObject.transform.position);
                    
                    if (minDistance > currentDistance)
                    {
                        minDistance = currentDistance;
                        nearestUseObject = useObject;
                    }
                }
            }

            if (nearestUseObject != null)
            {
                if (nearestUseObject != _previousUseObject)
                {
                    ButtonOn(nearestUseObject);
                }
            }
            else if (_previousUseObject)
                ButtonOff();
        }

        private void ButtonOn(UseObject nearestUseObject)
        {
            GameData.UseButton.gameObject.SetActive(true);
            EventBus.OnSubmit = () => Use(nearestUseObject);
            _previousUseObject = nearestUseObject;
        }
        
        private void ButtonOff()
        {
            EventBus.OnSubmit = null;
            GameData.UseButton.gameObject.SetActive(false);
            _previousUseObject = null;
        }

        private void Use(UseObject nearestUseObject)
        {
            GameData.UseButton.gameObject.SetActive(false);
            EventBus.OnSubmit = null;
            nearestUseObject.Use();
        }
    }
}