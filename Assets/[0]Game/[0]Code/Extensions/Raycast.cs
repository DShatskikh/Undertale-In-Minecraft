using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Super_Auto_Mobs
{
    public static class Raycast
    {
        public static GameObject PointerRaycast(Vector2 position)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> resultsData = new List<RaycastResult>();
            pointerData.position = position;
            EventSystem.current.RaycastAll(pointerData, resultsData);

            if(resultsData.Count > 0)
            {
                return resultsData[0].gameObject;
            }

            return null;
        }
        
        public static T GetRayUIObject<T>() where T : MonoBehaviour
        {
            var pointer = Raycast.PointerRaycast(Input.mousePosition); 
            
            if (pointer == null)
                return null;

            if (pointer.TryGetComponent(out T value))
                return value;
            
            return null;
        }
        
        public static T GetRayColliderObject<T>() where T : MonoBehaviour
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
                    
            if (hitCollider != null && hitCollider.TryGetComponent(out T value)) 
            {
                return value;
            }

            return null;
        }
    }
}