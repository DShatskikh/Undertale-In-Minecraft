using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class EndingsManager : MonoBehaviour
    {
        [SerializeField]
        public GameObject _badEnd;
        
        [SerializeField]
        public GameObject _goodEnd;
        
        [FormerlySerializedAs("_secretEnd")] [SerializeField]
        public GameObject _strangeEnd;

        public void End(EndEnum endEnum)
        {
            GameData.LocationsManager.gameObject.SetActive(false);
            GameData.CompanionsManager.gameObject.SetActive(false);
            
            switch (endEnum)
            {
                case EndEnum.Bad:
                    _badEnd.SetActive(true);
                    break;
                case EndEnum.Good:
                    _goodEnd.SetActive(true);
                    break;
                case EndEnum.Strange:
                    _strangeEnd.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(endEnum), endEnum, null);
            }
        }
    }
}