using UnityEngine;

namespace Game
{
    public class ReplaceObject : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private GameObject _gameObject;

        [SerializeField] 
        private Transform _parent;
        
        private void Start()
        {
            var newObject = Instantiate(_prefab, _parent);
            newObject.transform.position = _gameObject.transform.position;
            
            Destroy(_gameObject);
            Destroy(gameObject);
        }
    }
}