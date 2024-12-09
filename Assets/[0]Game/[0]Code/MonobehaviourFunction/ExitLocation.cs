using UnityEngine;

namespace Game
{
    public class ExitLocation : MonoBehaviour
    {
        [SerializeField]
        private string _nextLocationName;

        [SerializeField]
        private int _pointIndex;

        [SerializeField]
        private LocationEnum _location;

        public void Exit()
        {
            GameData.LocationsManager.SwitchLocation(_nextLocationName, _pointIndex);
        }
    }
}