using UnityEngine;

namespace Game
{
    public class ExitLocation : MonoBehaviour
    {
        [SerializeField]
        private Location _location;

        [SerializeField]
        private int _pointIndex;
        
        public void Exit()
        {
            GameData.LocationsManager.SwitchLocation((int)_location, _pointIndex);
        }
        
        public enum Location
        {
            HerobrineHome,
            World,
            
        }
    }
}