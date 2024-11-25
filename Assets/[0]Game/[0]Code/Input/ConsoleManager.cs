using PixelCrushers.DialogueSystem;
using QFSW.QC;
using UnityEngine;

namespace Game
{
    public class ConsoleManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _console;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.RightControl))
                _console.SetActive(!_console.activeSelf);
        }

        [Command("LUA")]
        private void LuaCommand(string command)
        {
            Lua.Run(command);
        }
    }
}