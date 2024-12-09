using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;

namespace PixelCrushers.DialogueSystem.InventoryEngineSupport
{

    /// <summary>
    /// Updates the quest tracker HUD and invokes a UnityEvent when an Inventory's content changes.
    /// Adds option to integrate Dialogue System saving with Inventory Engine SaveLoadManager.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/Inventory Engine/Dialogue System Inventory Event Listener")]
    [RequireComponent(typeof(Inventory))]
    public class DialogueSystemInventoryEventListener : MonoBehaviour, MMEventListener<MMInventoryEvent>, MMEventListener<MMGameEvent>
    {

        [Tooltip("Update the quest tracker when the inventory's content changes.")]
        public bool updateQuestTracker = true;

        [Tooltip("Save & load Dialogue System data when More Mountains SaveLoadManager requests. If using DialogueSystemCorgiEventListener, UNtick this on one or the other.")]
        public bool handleMMSaveLoadEvents = false;

        public UnityEvent onContentChanged = new UnityEvent();

        /// <summary>
        /// On enable, we start listening for MMGameEvents.
        /// </summary>
        protected virtual void OnEnable()
        {
            this.MMEventStartListening<MMInventoryEvent>();
            if (handleMMSaveLoadEvents) this.MMEventStartListening<MMGameEvent>();
        }

        /// <summary>
        /// On disable, we stop listening for MMGameEvents.
        /// </summary>
        protected virtual void OnDisable()
        {
            this.MMEventStopListening<MMInventoryEvent>();
            if (handleMMSaveLoadEvents) this.MMEventStopListening<MMGameEvent>();
        }


        public virtual void OnMMEvent(MMInventoryEvent eventType)
        {
            if (eventType.TargetInventoryName != name) return;
            switch (eventType.InventoryEventType)
            {
                case MMInventoryEventType.ContentChanged:
                    if (updateQuestTracker) DialogueManager.SendUpdateTracker();
                    onContentChanged.Invoke();
                    break;
            }
        }

        public virtual void OnMMEvent(MMGameEvent gameEvent)
        {
            if (gameEvent.EventName == "Save")
            {
                SaveDialogueSystem();
            }
            if (gameEvent.EventName == "Load")
            {
                LoadDialogueSystem();
            }
        }

        protected const string _saveFolderName = "DialogueSystem/";
        protected const string _saveFileExtension = ".data";

        public void SaveDialogueSystem()
        {
            var data = SaveSystem.hasInstance
                ? SaveSystem.Serialize(SaveSystem.RecordSavedGameData())
                : PersistentDataManager.GetSaveData();
            MMSaveLoadManager.Save(data, gameObject.name + _saveFileExtension, _saveFolderName);

        }

        public void LoadDialogueSystem()
        {
            string data = (string)MMSaveLoadManager.Load(typeof(string), gameObject.name + _saveFileExtension, _saveFolderName);
            if (SaveSystem.hasInstance)
            {
                SaveSystem.ApplySavedGameData(SaveSystem.Deserialize<SavedGameData>(data));
            }
            else
            {
                PersistentDataManager.ApplySaveData(data);
            }
        }

    }
}
