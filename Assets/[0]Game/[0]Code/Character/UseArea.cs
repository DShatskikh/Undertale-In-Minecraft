using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class UseArea : MonoBehaviour
    {
        [SerializeField]
        private float _radius;

        private MonoBehaviour _previousUseObject;

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
            MonoBehaviour nearestUseObject = null;

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
                else if (collider.TryGetComponent(out Usable usable))
                {
                    var currentDistance = Vector2.Distance(transform.position, usable.transform.position);
                    
                    if (minDistance > currentDistance)
                    {
                        minDistance = currentDistance;
                        nearestUseObject = usable;
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

        private void ButtonOn(MonoBehaviour nearestUseObject)
        {
            GameData.UseButton.gameObject.SetActive(true);
            EventBus.SubmitUp = () => Use(nearestUseObject);
            _previousUseObject = nearestUseObject;
        }
        
        private void ButtonOff()
        {
            EventBus.SubmitUp = null;
            GameData.UseButton.gameObject.SetActive(false);
            _previousUseObject = null;
        }

        private void Use(MonoBehaviour nearestUseObject)
        {
            GameData.UseButton.gameObject.SetActive(false);
            EventBus.SubmitUp = null;

            if (nearestUseObject is UseObject useObject) 
                useObject.Use();
            else if (nearestUseObject is Usable currentUsable)
            {
                currentUsable.OnUseUsable();
                if (currentUsable != null)
                    currentUsable.gameObject.BroadcastMessage("OnUse", transform,
                        SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}