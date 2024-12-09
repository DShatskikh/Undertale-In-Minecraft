using UnityEngine;

namespace Game
{
    public class ItemPicker : MoreMountains.InventoryEngine.ItemPicker, IUseObject
    {
        public override void OnTriggerEnter2D(Collider2D collider) { }

        public void Use()
        {
            Pick(Item.TargetInventoryName);
        }
    }
}