using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEditor.Localization;
using UnityEditorInternal;

namespace PixelCrushers.DialogueSystem.LocalizationPackageSupport
{

    /// <summary>
    /// Custom editor window to populate Localization Package string table with
    /// actor names and dialogue entry text. At runtime, DialogueSystemLocalizationPackageBridge
    /// will read localized values from string table.
    /// </summary>
    public class DialogueToLocalizationTableWindow : EditorWindow
    {

        [MenuItem("Tools/Pixel Crushers/Dialogue System/Third Party/Localization/Dialogue To Localization Table", false, 3)]
        public static void Init()
        {
            var window = EditorWindow.GetWindow(typeof(DialogueToLocalizationTableWindow), false, "DS To Loc") as DialogueToLocalizationTableWindow;
            window.minSize = new Vector2(300, 280);
        }

        private const string PrefsKey = "DialogueSystem.DSToLTPrefs";

        [Serializable]
        public class Prefs
        {
            public List<string> databaseGuids = new List<string>();
            public List<string> textTableGuids = new List<string>();
            public string localizationSettingsGuid;
            public string stringTableCollectionGuid;
            public string defaultLocaleGuid;
            public string guidFieldTitle = "Guid";
        }

        private Prefs prefs;
        private LocalizationSettings localizationSettings;
        private StringTableCollection stringTableCollection;
        private Locale defaultLocale;
        private List<DialogueDatabase> databases = new List<DialogueDatabase>();
        private List<TextTable> textTables = new List<TextTable>();
        private ReorderableList databasesList;
        private ReorderableList textTablesList;
        private Vector2 scrollPosition = Vector2.zero;

        private void OnEnable()
        {
            if (EditorPrefs.HasKey(PrefsKey))
            {
                prefs = JsonUtility.FromJson<Prefs>(EditorPrefs.GetString(PrefsKey));
            }
            if (prefs == null) prefs = new Prefs();

            databases.Clear();
            foreach (var databaseGuid in prefs.databaseGuids)
            {
                if (!string.IsNullOrEmpty(databaseGuid))
                {
                    var database = AssetDatabase.LoadAssetAtPath<DialogueDatabase>(AssetDatabase.GUIDToAssetPath(databaseGuid));
                    if (database != null)
                    {
                        databases.Add(database);
                    }
                }
            }
            textTables.Clear();
            foreach (var textTableGuid in prefs.textTableGuids)
            {
                if (!string.IsNullOrEmpty(textTableGuid))
                {
                    var textTable = AssetDatabase.LoadAssetAtPath<TextTable>(AssetDatabase.GUIDToAssetPath(textTableGuid));
                    if (textTable != null)
                    {
                        textTables.Add(textTable);
                    }
                }
            }
            localizationSettings = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(AssetDatabase.GUIDToAssetPath(prefs.localizationSettingsGuid));
            stringTableCollection = AssetDatabase.LoadAssetAtPath<StringTableCollection>(AssetDatabase.GUIDToAssetPath(prefs.stringTableCollectionGuid));
            defaultLocale = AssetDatabase.LoadAssetAtPath<Locale>(AssetDatabase.GUIDToAssetPath(prefs.defaultLocaleGuid));
        }

        private void OnDisable()
        {
            prefs.databaseGuids.Clear();
            foreach (var database in databases)
            {
                prefs.databaseGuids.Add((database != null) ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(database)) : string.Empty);
            }
            prefs.textTableGuids.Clear();
            foreach (var textTable in textTables)
            {
                prefs.textTableGuids.Add((textTable != null) ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(textTable)) : string.Empty);
            }
            prefs.localizationSettingsGuid = (localizationSettings != null) ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(localizationSettings)) : string.Empty;
            prefs.stringTableCollectionGuid = (stringTableCollection != null) ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(stringTableCollection)) : string.Empty;
            prefs.defaultLocaleGuid = (defaultLocale != null) ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(defaultLocale)) : string.Empty;
            EditorPrefs.SetString(PrefsKey, JsonUtility.ToJson(prefs));
        }

        private void OnGUI()
        {
            try
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                if (databasesList == null)
                {
                    databasesList = new ReorderableList(databases, typeof(DialogueDatabase), true, true, true, true);
                    databasesList.drawHeaderCallback += OnDrawDatabasesListHeader;
                    databasesList.drawElementCallback += OnDrawDatabasesListElement;
                    databasesList.onAddCallback += OnAddDatabase;
                }
                databasesList.DoLayoutList();
                if (textTablesList == null)
                {
                    textTablesList = new ReorderableList(textTables, typeof(TextTable), true, true, true, true);
                    textTablesList.drawHeaderCallback += OnDrawTextTablesListHeader;
                    textTablesList.drawElementCallback += OnDrawTextTablesListElement;
                    textTablesList.onAddCallback += OnAddTextTable;
                }
                textTablesList.DoLayoutList();
                prefs.guidFieldTitle = EditorGUILayout.TextField(new GUIContent("Unique Field Title", "Field title to use/create in dialogue database to uniquely and persistently identify each Key in string table."), prefs.guidFieldTitle);
                localizationSettings = EditorGUILayout.ObjectField("Localization Settings", localizationSettings, typeof(LocalizationSettings), false) as LocalizationSettings;
                stringTableCollection = EditorGUILayout.ObjectField("String Table", stringTableCollection, typeof(StringTableCollection), false) as StringTableCollection;
                defaultLocale = EditorGUILayout.ObjectField("Default Locale", defaultLocale, typeof(Locale), false) as Locale;
                EditorGUI.BeginDisabledGroup(!HasAnyDatabases() || stringTableCollection == null || defaultLocale == null || string.IsNullOrEmpty(prefs.guidFieldTitle));
                if (GUILayout.Button("Dialogue System To String Table"))
                {
                    CopyDialogueSystemToStringTable();
                }
                if (GUILayout.Button("String Table To Dialogue System"))
                {
                    CopyStringTableToDialogueSystem();
                }
                EditorGUI.EndDisabledGroup();
            }
            finally
            {
                EditorGUILayout.EndScrollView();
            }
        }

        private bool HasAnyDatabases()
        {
            return databases.Find(x => x != null) != null;
        }

        private void OnDrawDatabasesListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Databases");
        }

        private void OnDrawDatabasesListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (!(0 <= index && index < databases.Count)) return;
            databases[index] = EditorGUI.ObjectField(rect, databases[index], typeof(DialogueDatabase), true) as DialogueDatabase;
        }

        private void OnAddDatabase(ReorderableList list)
        {
            databases.Add(null);
        }

        private void OnDrawTextTablesListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Text Tables");
        }

        private void OnDrawTextTablesListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (!(0 <= index && index < textTables.Count)) return;
            textTables[index] = EditorGUI.ObjectField(rect, textTables[index], typeof(TextTable), true) as TextTable;
        }

        private void OnAddTextTable(ReorderableList list)
        {
            textTables.Add(null);
        }

        private void CopyDialogueSystemToStringTable()
        {
            CopyDialogueDatabasesToStringTable();
            CopyTextTablesToStringTable();
        }

        private void CopyStringTableToDialogueSystem()
        {
            CopyStringTableToDialogueDatabases();
            CopyStringTableToTextTables();
        }

        private void CopyDialogueDatabasesToStringTable()
        {
            try
            {

                Undo.RecordObjects(new UnityEngine.Object[] { stringTableCollection.SharedData, stringTableCollection }, "Modified table");

                var table = stringTableCollection.StringTables.First(x => x.LocaleIdentifier == defaultLocale.Identifier);
                if (table == null)
                {
                    Debug.LogError("Can't find string table for locale " + defaultLocale.Identifier.Code);
                    return;
                }

                var hasRecordedDatabaseUndo = false;
                float total = 0;
                foreach (var database in databases)
                {
                    if (database == null) continue;
                    total += database.actors.Count + database.conversations.Count;
                    var quests = database.items.FindAll(x => !x.IsItem);
                    total += quests.Count;
                }
                int progress = 0;

                // Actor display names:
                foreach (var database in databases)
                {
                    if (database == null) continue;
                    foreach (var actor in database.actors)
                    {
                        progress++;
                        if (EditorUtility.DisplayCancelableProgressBar("Dialogue To String Table", actor.Name, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }

                        // Get actor guid:
                        var field = Field.Lookup(actor.fields, prefs.guidFieldTitle);
                        if (field == null)
                        {
                            if (!hasRecordedDatabaseUndo)
                            {
                                hasRecordedDatabaseUndo = true;
                                Undo.RecordObject(database, "Modify database");
                            }
                            field = new Field(prefs.guidFieldTitle, Guid.NewGuid().ToString(), FieldType.Text);
                            actor.fields.Add(field);
                        }
                        else if (string.IsNullOrEmpty(field.value))
                        {
                            field.value = Guid.NewGuid().ToString();
                        }

                        var actorDisplayName = actor.FieldExists("Display Name") ? actor.LookupValue("Display Name") : actor.Name;
                        table.AddEntry(field.value, actorDisplayName);
                    }
                }

                // Quests:
                foreach (var database in databases)
                {
                    if (database == null) continue;
                    foreach (var quest in database.items)
                    {
                        if (quest.IsItem) continue;
                        progress++;
                        if (EditorUtility.DisplayCancelableProgressBar("Dialogue To String Table", quest.Name, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }

                        // Get quest guid:
                        var field = Field.Lookup(quest.fields, prefs.guidFieldTitle);
                        if (field == null)
                        {
                            if (!hasRecordedDatabaseUndo)
                            {
                                hasRecordedDatabaseUndo = true;
                                Undo.RecordObject(database, "Modify database");
                            }
                            field = new Field(prefs.guidFieldTitle, Guid.NewGuid().ToString(), FieldType.Text);
                            quest.fields.Add(field);
                        }
                        else if (string.IsNullOrEmpty(field.value))
                        {
                            field.value = Guid.NewGuid().ToString();
                        }

                        var questDisplayName = quest.FieldExists("Display Name") ? quest.LookupValue("Display Name") : quest.Name;
                        table.AddEntry(field.value, questDisplayName);

                        var description = quest.LookupValue("Description");
                        table.AddEntry(field.value + "_Description", description);
                        var successDescription = quest.LookupValue("Success Description");
                        if (!string.IsNullOrEmpty(successDescription)) table.AddEntry(field.value + "_SuccessDescription", successDescription);
                        var failureDescription = quest.LookupValue("Failure Description");
                        if (!string.IsNullOrEmpty(failureDescription)) table.AddEntry(field.value + "_FailureDescription", failureDescription);

                        var entryCount = quest.LookupInt("Entry Count");
                        for (int i = 1; i <= entryCount; i++)
                        {
                            table.AddEntry(field.value + "_Entry_" + i, quest.LookupValue("Entry " + i));
                        }
                    }
                }

                // Conversations:
                foreach (var database in databases)
                {
                    if (database == null) continue;
                    foreach (var conversation in database.conversations)
                    {
                        progress++;
                        if (EditorUtility.DisplayCancelableProgressBar("Dialogue To String Table", conversation.Title, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }

                        foreach (var entry in conversation.dialogueEntries)
                        {
                            // Get dialogue entry guid:
                            var field = Field.Lookup(entry.fields, prefs.guidFieldTitle);
                            if (field == null)
                            {
                                if (!hasRecordedDatabaseUndo)
                                {
                                    hasRecordedDatabaseUndo = true;
                                    Undo.RecordObject(database, "Modify database");
                                }
                                field = new Field(prefs.guidFieldTitle, Guid.NewGuid().ToString(), FieldType.Text);
                                entry.fields.Add(field);
                            }
                            else if (string.IsNullOrEmpty(field.value))
                            {
                                field.value = Guid.NewGuid().ToString();
                            }

                            // Add localized entries:
                            table.AddEntry(field.value, entry.DialogueText);
                            if (!string.IsNullOrEmpty(entry.MenuText))
                            {
                                table.AddEntry(field.value + "_MenuText", entry.MenuText);
                            }
                        }
                    }
                }

                Debug.Log("Populated Localization Package string table " + stringTableCollection.name + " with dialogue database fields.", stringTableCollection);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private void CopyStringTableToDialogueDatabases()
        {
            try
            {
                float total = databases.Count;
                int progress = 0;
                foreach (var database in databases)
                {
                    if (database == null) continue;
                    Undo.RecordObject(database, "Localization to Database");
                    progress++;

                    // Actor display names:
                    foreach (var actor in database.actors)
                    {
                        if (EditorUtility.DisplayCancelableProgressBar("String Table To Dialogue", actor.Name, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }

                        // Get actor guid:
                        var field = Field.Lookup(actor.fields, prefs.guidFieldTitle);
                        if (field == null || string.IsNullOrEmpty(field.value)) continue;
                        foreach (var stringTable in stringTableCollection.StringTables)
                        {
                            var stringTableEntry = stringTable.GetEntry(field.value);
                            if (stringTableEntry == null) continue;
                            var displayNameFieldTitle = "Display Name";
                            if (stringTable.LocaleIdentifier != defaultLocale.Identifier)
                            {
                                displayNameFieldTitle += $" {stringTable.LocaleIdentifier.Code}";
                            }
                            Field.SetValue(actor.fields, displayNameFieldTitle, stringTableEntry.LocalizedValue, FieldType.Localization);
                        }
                    }

                    // Quests:
                    foreach (var quest in database.items)
                    {
                        if (quest.IsItem) continue;
                        if (EditorUtility.DisplayCancelableProgressBar("String Table To Dialogue", quest.Name, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }

                        // Get quest guid:
                        var field = Field.Lookup(quest.fields, prefs.guidFieldTitle);
                        if (field == null || string.IsNullOrEmpty(field.value)) continue;
                        foreach (var stringTable in stringTableCollection.StringTables)
                        {
                            CopyStringTableEntryToField(stringTable, field.value, quest.fields, "Display Name");
                            CopyStringTableEntryToField(stringTable, field.value + "_Description", quest.fields, "Description");
                            CopyStringTableEntryToField(stringTable, field.value + "_SuccessDescription", quest.fields, "Success Description");
                            CopyStringTableEntryToField(stringTable, field.value + "_FailureDescription", quest.fields, "Failure Description");
                            var entryCount = quest.LookupInt("Entry Count");
                            for (int i = 1; i <= entryCount; i++)
                            {
                                CopyStringTableEntryToField(stringTable, field.value + "_Entry_" + i, quest.fields, "Entry " + i);
                            }
                        }
                    }

                    // Conversations:
                    if (database == null) continue;
                    foreach (var conversation in database.conversations)
                    {
                        if (EditorUtility.DisplayCancelableProgressBar("String Table To Dialogue", conversation.Title, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }

                        foreach (var entry in conversation.dialogueEntries)
                        {
                            // Get dialogue entry guid:
                            var field = Field.Lookup(entry.fields, prefs.guidFieldTitle);
                            if (field == null || string.IsNullOrEmpty(field.value)) continue;
                            // Add localized entries:
                            foreach (var stringTable in stringTableCollection.StringTables)
                            {
                                if (stringTable == null) continue;
                                var stringTableEntry = stringTable.GetEntry(field.value);
                                if (stringTableEntry != null)
                                {
                                    if (stringTable.LocaleIdentifier == defaultLocale.Identifier)
                                    {
                                        Field.SetValue(entry.fields, "Dialogue Text", stringTableEntry.LocalizedValue);
                                    }
                                    else
                                    {
                                        Field.SetValue(entry.fields, stringTable.LocaleIdentifier.Code, stringTableEntry.LocalizedValue, FieldType.Localization);
                                    }
                                }
                                stringTableEntry = stringTable.GetEntry(field.value + "_MenuText");
                                if (stringTableEntry != null)
                                {
                                    if (stringTable.LocaleIdentifier == defaultLocale.Identifier)
                                    {
                                        Field.SetValue(entry.fields, "Menu Text", stringTableEntry.LocalizedValue);
                                    }
                                    else
                                    {
                                        Field.SetValue(entry.fields, "Menu Text " + stringTable.LocaleIdentifier.Code, stringTableEntry.LocalizedValue, FieldType.Localization);
                                    }
                                }
                            }
                        }
                    }
                }
                Debug.Log("Copied Localization Package string table " + stringTableCollection.name + " values back to dialogue database.", stringTableCollection);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private void CopyStringTableEntryToField(UnityEngine.Localization.Tables.StringTable stringTable, string entryKey, List<Field> fields, string fieldTitle)
        {
            var stringTableEntry = stringTable.GetEntry(entryKey);
            if (stringTableEntry == null) return;
            if (stringTable.LocaleIdentifier != defaultLocale.Identifier)
            {
                fieldTitle += $" {stringTable.LocaleIdentifier.Code}";
            }
            Field.SetValue(fields, fieldTitle, stringTableEntry.LocalizedValue, FieldType.Localization);
        }

        private void CopyTextTablesToStringTable()
        {
            try
            {

                var table = stringTableCollection.StringTables.First(x => x.LocaleIdentifier == defaultLocale.Identifier);
                if (table == null)
                {
                    Debug.LogError("Can't find string table for locale " + defaultLocale.Identifier.Code);
                    return;
                }

                float total = 0;
                foreach (var textTable in textTables)
                {
                    if (textTable == null) continue;
                    total += textTable.fields.Count;
                }

                int progress = 0;
                foreach (var textTable in textTables)
                {
                    if (textTable == null) continue;
                    foreach (var field in textTable.fields.Values)
                    {
                        progress++;
                        if (string.IsNullOrEmpty(field.fieldName) || field.texts == null || field.texts.Count == 0) continue;
                        if (EditorUtility.DisplayCancelableProgressBar("Text Table To String Table", field.fieldName, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }
                        table.AddEntry(field.fieldName, field.texts[0]);
                    }
                }

                Debug.Log("Populated Localization Package string table " + stringTableCollection.name + " with text table fields.", stringTableCollection);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private void CopyStringTableToTextTables()
        {
            try
            {

                var table = stringTableCollection.StringTables.First(x => x.LocaleIdentifier == defaultLocale.Identifier);
                if (table == null)
                {
                    Debug.LogError("Can't find string table for locale " + defaultLocale.Identifier.Code);
                    return;
                }

                float total = 0;
                foreach (var textTable in textTables)
                {
                    if (textTable == null) continue;
                    total += textTable.fields.Count;
                }

                int progress = 0;
                foreach (var textTable in textTables)
                {
                    if (textTable == null) continue;

                    var languageCodeToTextTableID = new Dictionary<string, int>();
                    foreach (var kvp in textTable.languages)
                    {
                        languageCodeToTextTableID[kvp.Key] = kvp.Value;
                    }
                    foreach (var field in textTable.fields.Values)
                    {
                        progress++;
                        if (string.IsNullOrEmpty(field.fieldName) || field.texts == null || field.texts.Count == 0) continue;
                        if (EditorUtility.DisplayCancelableProgressBar("String Table To Text Table", field.fieldName, progress / total))
                        {
                            Debug.Log("Cancelled.");
                            return;
                        }
                        foreach (var stringTable in stringTableCollection.StringTables)
                        {
                            var stringTableEntry = stringTable.GetEntry(field.fieldName);
                            if (stringTableEntry == null) continue;
                            int languageID;
                            if (languageCodeToTextTableID.TryGetValue(stringTable.LocaleIdentifier.Code, out languageID))
                            {
                                textTable.SetFieldTextForLanguage(field.fieldName, languageID, stringTableEntry.LocalizedValue);
                            }
                        }
                    }
                }

                Debug.Log("Copied Localization Package string table " + stringTableCollection.name + " back to text table fields.", stringTableCollection);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

    }
}

