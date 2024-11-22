using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class CharacterMenu : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;

        [SerializeField]
        private SpriteRenderer _freak;
        
        [SerializeField]
        private SpriteRenderer _cylinder;
        
        [SerializeField]
        private SpriteRenderer _mysteryCylinder;
        
        [SerializeField]
        private SpriteRenderer _eliteCylinder;

        [SerializeField]
        private Transform[] _points;
        
        private static readonly int StateHash = Animator.StringToHash("State");
        
        private void Start()
        {
            Dance();
            
            FreakShow(Lua.IsTrue("Variable[\"IsBavBadEnding\"] >= 4"));
            CylinderShow(Lua.IsTrue("Variable[\"IsCylinder\"] == true"));
            MysticalCylinderShow(Lua.IsTrue("Variable[\"IsMysteryCylinder\"] == true"));
            EliteCylinderShow(Lua.IsTrue("Variable[\"IsEliteCylinder\"] == true"));
            
            var hats = new List<SpriteRenderer>() { _eliteCylinder, _cylinder, _mysteryCylinder };

            foreach (var point in _points)
            {
                foreach (var hat in hats)
                {
                    if (hat.gameObject.activeSelf)
                    {
                        hat.transform.position = point.position;
                        hat.transform.eulerAngles = point.eulerAngles;
                        hats.Remove(hat);
                        break;
                    }
                }
            }
        }

        private void Dance() => 
            _animator.SetFloat(StateHash, 1);

        private void CylinderShow(bool isShow) => 
            _cylinder.gameObject.SetActive(isShow);

        private void MysticalCylinderShow(bool isShow) => 
            _mysteryCylinder.gameObject.SetActive(isShow);

        private void EliteCylinderShow(bool isShow) => 
            _eliteCylinder.gameObject.SetActive(isShow);

        private void FreakShow(bool isShow) => 
            _freak.gameObject.SetActive(isShow);
    }
}