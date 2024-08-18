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
            Prison,
            RoomDeveloper,
            TrueSecret,
            Cafe,
            HerobrineLaboratory,
            HerobrineLaboratoryArena,
            TrueSecretStatue,
            WismanRoom,
            OfficeRoof,
            Museum,
            MuseumBasement,
            MuseumSecret,
            Office,
            Nether,
            NetherHouse,
            NetherHouseSecret,
            ErrorWorld,
            ErrorPrison,
            ErrorCafe,
            ErrorHerobrineHome,
            ErrorMuseum,
            PalesosWorld,
        }
    }
}