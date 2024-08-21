using UnityEngine;

namespace Game
{
    public class ExitLocation : MonoBehaviour
    {
        [SerializeField]
        private LocationEnum _location;

        [SerializeField]
        private int _pointIndex;
        
        public void Exit()
        {
            GameData.LocationsManager.SwitchLocation((int)_location, _pointIndex);
        }
    }
}