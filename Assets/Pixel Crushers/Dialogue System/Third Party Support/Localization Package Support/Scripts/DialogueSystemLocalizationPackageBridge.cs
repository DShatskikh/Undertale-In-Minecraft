using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace PixelCrushers.DialogueSystem.LocalizationPackageSupport
{

    /// <summary>
    /// Reads localized actor display names and dialogue entry text from
    /// Localization Package string table.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/UI/Misc/Dialogue System Localization Package Bridge")]
    public class DialogueSystemLocalizationPackageBridge : MonoBehaviour
    {

        [Tooltip("Assign string tables that contain dialogue translations to this list.")]
        public List<LocalizedStringTable> localizedStringTables;

        [Tooltip("Default locale that game starts in.")]
        public Locale defaultLocale;

        [Tooltip("Title of dialogue entry field that corresponds to key in string table.")]
        public string uniqueFieldTitle = "Guid";

        [Tooltip("When Dialogue System attempts to localize non-dialogue text, use localized string tables instead of Dialogue System's default behavior of using Text Table assets.")]
        public bool replaceGetLocalizedText = false;

        [Tooltip("Update onscreen dialogue UI as soon as locale changes, not on next line. Limitation: Works with standard dialogue UI in single conversations (not simultaneous conversations). Override UpdateDialogueUI add different behavior.")]
        public bool updateDialogueUIImmediately = true;

        protected List<UnityEngine.Localization.Tables.StringTable> tables = new List<UnityEngine.Localization.Tables.StringTable>();

        protected virtual IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;
            yield return new WaitForEndOfFrame();
            CacheStringTables();
            UpdateActorDisplayNames();
            Localization.language = LocalizationSettings.SelectedLocale.Identifier.Code;
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;

            if (replaceGetLocalizedText && DialogueManager.instance.overrideGetLocalizedText == null)
            {
                DialogueManager.instance.overrideGetLocalizedText = GetLocalizedTextFromStringTables;
            }
        }

        protected virtual void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        public virtual void CacheStringTables()
        {
            tables.Clear();
            foreach (var table in localizedStringTables)
            {
                if (table != null)
                {
                    tables.Add(table.GetTable());
                }
            }
        }

        protected virtual void OnSelectedLocaleChanged(Locale locale)
        {
            if (!Application.isPlaying) return;
            CacheStringTables();
            UpdateActorDisplayNames();
            if (updateDialogueUIImmediately) UpdateDialogueUI();
            Localization.language = LocalizationSettings.SelectedLocale.Identifier.Code;
        }

        public virtual void UpdateActorDisplayNames()
        {
            var locale = LocalizationSettings.SelectedLocale;
            Localization.language = locale.Identifier.Code;
            foreach (var actor in DialogueManager.masterDatabase.actors)
            {
                var guid = actor.LookupValue(uniqueFieldTitle);
                if (!string.IsNullOrEmpty(guid))
                {
                    foreach (var table in tables)
                    {
                        var stringTableEntry = table[guid];
                        if (stringTableEntry != null)
                        {
                            var fieldTitle = (locale == defaultLocale) ? "Display Name" : ("Display Name " + locale.Identifier.Code);
                            DialogueLua.SetActorField(actor.Name, fieldTitle, stringTableEntry.LocalizedValue);
                            break;
                        }
                    }
                }
            }
        }

        public virtual void OnBarkLine(Subtitle subtitle)
        {
            LocalizeSubtitle(subtitle);
        }

        public virtual void OnConversationLine(Subtitle subtitle)
        {
            LocalizeSubtitle(subtitle);
        }

        public virtual void LocalizeSubtitle(Subtitle subtitle)
        {
            if (string.IsNullOrEmpty(subtitle.formattedText.text)) return;
            var guid = Field.LookupValue(subtitle.dialogueEntry.fields, uniqueFieldTitle);
            if (string.IsNullOrEmpty(guid)) return;
            foreach (var table in tables)
            {
                var stringTableEntry = table[guid];
                if (stringTableEntry != null)
                {
                    var localizedValue = stringTableEntry.LocalizedValue;
                    subtitle.formattedText = FormattedText.Parse(localizedValue);
                    break;
                }
            }
        }

        public virtual void OnConversationResponseMenu(Response[] responses)
        {
            foreach (Response response in responses)
            {
                var guid = Field.LookupValue(response.destinationEntry.fields, uniqueFieldTitle);
                if (string.IsNullOrEmpty(guid)) continue;
                foreach (var table in tables)
                {
                    var stringTableEntry = table[guid + "_MenuText"];
                    if (stringTableEntry != null)
                    {
                        response.formattedText = FormattedText.Parse(stringTableEntry.LocalizedValue);
                        break;
                    }
                    else
                    {
                        stringTableEntry = table[guid];
                        if (stringTableEntry != null)
                        {
                            response.formattedText = FormattedText.Parse(stringTableEntry.LocalizedValue);
                            break;
                        }
                    }
                }
            }
        }

        protected virtual void UpdateDialogueUI()
        {
            if (!DialogueManager.IsConversationActive) return;
            var conversationElements = DialogueManager.standardDialogueUI.conversationUIElements;
            var state = DialogueManager.currentConversationState;
            LocalizeSubtitle(state.subtitle);
            DialogueActor dialogueActor;
            var panel = conversationElements.standardSubtitleControls.GetPanel(state.subtitle, out dialogueActor);
            panel.subtitleText.text = state.subtitle.formattedText.text;
            if (panel.portraitName != null)
            {
                var actor = DialogueManager.masterDatabase.GetActor(state.subtitle.speakerInfo.id);
                if (actor != null)
                {
                    panel.portraitName.text = DialogueLua.GetLocalizedActorField(actor.Name, "Display Name").asString;
                }
            }
            if (conversationElements.defaultMenuPanel.isOpen)
            {
                OnConversationResponseMenu(state.pcResponses);
                var target = (conversationElements.defaultMenuPanel.instantiatedButtons.Count > 0)
                    ? conversationElements.defaultMenuPanel.instantiatedButtons[0].GetComponent<StandardUIResponseButton>().target
                    : conversationElements.defaultMenuPanel.buttons[0].target;
                conversationElements.defaultMenuPanel.ShowResponses(state.subtitle, state.pcResponses, target);
            }
        }

        protected virtual string GetLocalizedTextFromStringTables(string s)
        {
            foreach (var table in tables)
            {
                var stringTableEntry = table[s];
                if (stringTableEntry != null)
                {
                    return stringTableEntry.LocalizedValue;
                }
            }
            return s;
        }

    }
}
