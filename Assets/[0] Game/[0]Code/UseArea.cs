using UnityEngine;

namespace Game
{
    public class UseArea : MonoBehaviour
    {
        [SerializeField]
        private float _radius;

        private UseObject _previousUseObject;

        private void Start()
        {
            GameData.UseButton.onClick.AddListener(() => GameData.UseButton.gameObject.SetActive(false));
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
                ButtonOn(nearestUseObject);
            else
                ButtonOff();
        }

        private void ButtonOn(UseObject nearestUseObject)
        {
            GameData.UseButton.gameObject.SetActive(true);
            GameData.UseButton.onClick.AddListener(nearestUseObject.Use);
            _previousUseObject = nearestUseObject;
        }
        
        private void ButtonOff()
        {
            if (_previousUseObject)
            {
                GameData.UseButton.onClick.RemoveListener(_previousUseObject.Use);
                
                if (GameData.UseButton)
                    GameData.UseButton.gameObject.SetActive(false);

                _previousUseObject = null;
            }
        }
    }
}