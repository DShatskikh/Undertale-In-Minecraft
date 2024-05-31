using System;
using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class UseArea : MonoBehaviour
    {
        public float radius = 5f;
        public LayerMask layerMask;

        private UseObject _nearestUseObject;

        void Update()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

            UseObject nearestUseObject = null;
            float minDistance = float.MaxValue;
            
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out UseObject useObject))
                {
                    var distance = Vector2.Distance(transform.position, collider.transform.position);
                    
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestUseObject = useObject;
                    }
                }
            }

            if (nearestUseObject != null)
            {
                EventBus.OnSubmit = null;
                GameData.Arrow.transform.position = (Vector2)nearestUseObject.transform.position.AddY(1) + nearestUseObject.Offset;
                GameData.Arrow.SetActive(true);
                EventBus.OnSubmit += () => Use(nearestUseObject);
            }
            else
            {
                GameData.Arrow.SetActive(false);
                EventBus.OnSubmit = null;
            }
        }

        private void OnDisable()
        {
            return;
            
            GameData.Arrow.SetActive(false);
            EventBus.OnSubmit = null;
        }

        private void Use(UseObject useObject)
        {
            EventBus.OnSubmit = null;
            useObject.Use();
            GameData.Arrow.SetActive(false);
        }
    }
}