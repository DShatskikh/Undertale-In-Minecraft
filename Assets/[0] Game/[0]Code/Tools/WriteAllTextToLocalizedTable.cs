using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using Random = UnityEngine.Random;

namespace Game
{
    public class WriteAllTextToLocalizedTable : MonoBehaviour
    {
        [SerializeField]
        private StringTable _monologueTable;
        
        [SerializeField]
        private StringTable _dialogueTable;
        
        [SerializeField]
        private StringTable _selectableTable;
        
        [ContextMenu("Загрузить Monologues")]
        private void WriteMonologues()
        {
            foreach (var monolog in FindObjectsByType<UseMonolog>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                var key = GetRandomKey();
                var tableEntries = new List<LocalizedString>();

                for (int i = 0; i < monolog.GetTexts.Length; i++)
                {
                    var tableEntry = _monologueTable.AddEntry(key + $"_{i + 1}", monolog.GetTexts[i]);
                    tableEntries.Add(new LocalizedString("Monologues", tableEntry.KeyId));
                }

                monolog.SetLocalizedStrings = tableEntries.ToArray();
                EditorUtility.SetDirty(monolog.gameObject);
                EditorUtility.SetDirty(monolog);
            }
            
            foreach (var monolog in FindObjectsByType<OpenMonolog>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                var key = GetRandomKey();
                var tableEntries = new List<LocalizedString>();

                for (int i = 0; i < monolog.GetTexts.Length; i++)
                {
                    var tableEntry = _monologueTable.AddEntry(key + $"_{i + 1}", monolog.GetTexts[i]);
                    tableEntries.Add(new LocalizedString("Monologues", tableEntry.KeyId));
                }

                monolog.SetLocalizedStrings = tableEntries.ToArray();
                EditorUtility.SetDirty(monolog.gameObject);
                EditorUtility.SetDirty(monolog);
            }

            AssetDatabase.SaveAssets();
            print("Загрузка диалогов завершилась успешно");
        }
        
        [ContextMenu("Загрузить Dialogues")]
        private void WriteDialogues()
        {
            foreach (var dialog in FindObjectsByType<UseDialog>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                var key = GetRandomKey();
            
                for (int i = 0; i < dialog.GetReplicas.Length; i++)
                {
                    var tableEntry = _dialogueTable.AddEntry(key + $"_{i + 1}", dialog.GetReplicas[i].Text);
                    dialog.GetReplicas[i].LocalizationString = new LocalizedString("Dialogues", tableEntry.KeyId);
                }
                
                EditorUtility.SetDirty(dialog.gameObject);
                EditorUtility.SetDirty(dialog);
            }
            
            AssetDatabase.SaveAssets();
            print("Загрузка диалогов завершилась успешно");
        }
        
        [ContextMenu("Загрузить Selected")]
        private void WriteSelected()
        {
            foreach (var select in FindObjectsByType<UseSelect>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                var key = GetRandomKey();

                var tableEntry = _selectableTable.AddEntry(key, select.GetText);
                select.SetTableEntry(new LocalizedString("Selected", tableEntry.KeyId));
                EditorUtility.SetDirty(select.gameObject);
                EditorUtility.SetDirty(select);
            }

            AssetDatabase.SaveAssets();
            print("Загрузка диалогов завершилась успешно");
        }

        private string GetRandomKey()
        {
            char[] alphabet = Enumerable.Range('A', 26).Select(c => (char)c).ToArray();
            var key = "";

            for (int i = 0; i < 4; i++) 
                key += alphabet[Random.Range(0, alphabet.Length)];

            return key;
        }
    }
}