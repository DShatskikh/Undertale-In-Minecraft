using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "DonationSlot", menuName = "Data/DonationSlot", order = 55)]
    public class DonationSlotConfig : ScriptableObject
    {
        public ItemConfig ItemConfig;
    }
}