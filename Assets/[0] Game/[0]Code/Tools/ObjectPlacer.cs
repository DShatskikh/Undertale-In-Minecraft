using Super_Auto_Mobs;
using UnityEngine;

namespace Game
{
    public class ObjectPlacer : MonoBehaviour
    {
        public GameObject center;
        public GameObject prefab;
        public int objectCount;
        public float radius;

        [ContextMenu("Создать обьекты")]
        private void PlaceObjects()
        {
            for (int i = 0; i < objectCount; i++)
            {
                float angle = i * Mathf.PI * 2 / objectCount;
                Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                GameObject obj = Instantiate(prefab, center.transform.position + pos,
                    Quaternion.identity);
                obj.transform.SetParent(center.transform);
                
                var direction = ((Vector2) center.transform.position - (Vector2)obj.transform.position).normalized;
                float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
                obj.transform.Rotate(0, 0, rotationZ);
            }
        }
    }
}