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

        public void End(Endings endings)
        {
            GameData.LocationsManager.gameObject.SetActive(false);
            GameData.CompanionsManager.gameObject.SetActive(false);
            
            switch (endings)
            {
                case Endings.Bad:
                    _badEnd.SetActive(true);
                    break;
                case Endings.Good:
                    _goodEnd.SetActive(true);
                    break;
                case Endings.Strange:
                    _strangeEnd.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(endings), endings, null);
            }
        }
    }
}