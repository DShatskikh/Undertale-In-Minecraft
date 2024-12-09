using UnityEngine;
using System.Collections;
using MoreMountains.InventoryEngine;

namespace PixelCrushers.DialogueSystem.InventoryEngineSupport
{

    /// <summary>
    /// This subclass of Inventory Engine's InventoryDetails component
    /// runs all text through Dialogue System localization. Add any text
    /// that you want localized to the text table assigned to the Dialogue Manager.
    /// </summary>
    public class LocalizeInventoryDetails : InventoryDetails
    {

        protected override IEnumerator FillDetailFields(InventoryItem item, float initialDelay)
        {
            yield return new WaitForSeconds(initialDelay);
            if (Title != null) { Title.text = DialogueManager.GetLocalizedText(item.ItemName); }
            if (ShortDescription != null) { ShortDescription.text = DialogueManager.GetLocalizedText(item.ShortDescription); }
            if (Description != null) { Description.text = DialogueManager.GetLocalizedText(item.Description); }
            if (Quantity != null) { Quantity.text = item.Quantity.ToString(); }
            if (Icon != null) { Icon.sprite = item.Icon; }
        }

        protected override IEnumerator FillDetailFieldsWithDefaults(float initialDelay)
        {
            yield return new WaitForSeconds(initialDelay);
            if (Title != null) { Title.text = DialogueManager.GetLocalizedText(DefaultTitle); }
            if (ShortDescription != null) { ShortDescription.text = DialogueManager.GetLocalizedText(DefaultShortDescription); }
            if (Description != null) { Description.text = DialogueManager.GetLocalizedText(DefaultDescription); }
            if (Quantity != null) { Quantity.text = DefaultQuantity; }
            if (Icon != null) { Icon.sprite = DefaultIcon; }
        }
    }
}
