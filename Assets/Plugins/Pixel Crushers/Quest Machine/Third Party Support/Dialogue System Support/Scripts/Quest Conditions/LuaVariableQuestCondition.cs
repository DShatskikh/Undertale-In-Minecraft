﻿// Copyright © Pixel Crushers. All rights reserved.

using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.QuestMachine.DialogueSystemSupport
{

    /// <summary>
    /// Quest condition that checks whether a Dialogue System variable
    /// is equal/less/greater than a specified value.
    /// </summary>
    public class LuaVariableQuestCondition : QuestCondition
    {

        public enum LuaConditionMode { Equal, LessThan, LessOrEqualTo, GreaterThan, GreaterOrEqualTo }

        [Tooltip("Check this Dialogue System variable.")]
        [VariablePopup]
        public string variable;

        [Tooltip("If value type is Int, use this comparison mode. String values always compare equality.")]
        public LuaConditionMode mode = LuaConditionMode.Equal;

        [Tooltip("Variable must be equal to this value.")]
        public MessageValue value;

        [Tooltip("Check Variable at this frequency in seconds.")]
        public float frequencyToCheck = 1;

        public override void StartChecking(System.Action trueAction)
        {
            base.StartChecking(trueAction);
            if (IsLuaVariableConditionTrue())
            {
                SetTrue();
            }
            else if (frequencyToCheck > 0)
            {
                TimedCallbackManager.StartCallback(OnCallback, frequencyToCheck);
            }
        }

        public override void StopChecking()
        {
            base.StopChecking();
            TimedCallbackManager.StopCallback(OnCallback);
        }

        public void OnCallback()
        {
            if (IsLuaVariableConditionTrue()) SetTrue();
        }


        private bool IsLuaVariableConditionTrue()
        {
            if (string.IsNullOrEmpty(variable))
            {
                Debug.LogWarning("Quest Machine: Lua Variable Quest Condition - no variable is specified.");
            }
            else if (!DialogueLua.DoesVariableExist(variable))
            {
                Debug.LogWarning("Quest Machine: Lua Variable Quest Condition - variable '" + variable + "' doesn't exist in the Dialogue System.");
            }
            else if (value == null)
            {
                Debug.LogWarning("Quest Machine: Lua Variable Quest Condition - no comparison value is specified for variable '" + variable + "'.");
            }
            else
            {
                switch (value.valueType)
                {
                    case MessageValueType.Int:
                        var luaValue = DialogueLua.GetVariable(variable).AsInt;
                        switch (mode)
                        {
                            default:
                            case LuaConditionMode.Equal: 
                                return luaValue == value.intValue;
                            case LuaConditionMode.LessThan:
                                return luaValue < value.intValue;
                            case LuaConditionMode.LessOrEqualTo:
                                return luaValue <= value.intValue;
                            case LuaConditionMode.GreaterThan:
                                return luaValue > value.intValue;
                            case LuaConditionMode.GreaterOrEqualTo:
                                return luaValue >= value.intValue;
                        }
                    case MessageValueType.String:
                        return string.Equals(DialogueLua.GetVariable(variable).asString, value.stringValue);
                }
            }
            return false;
        }

        public override string GetEditorName()
        {
            if (value == null) return "Lua Variable Condition";
            if (value.valueType == MessageValueType.String) return "Lua variable " + variable + " == " + value;
            switch (mode)
            {
                default:
                case LuaConditionMode.Equal:
                    return "Lua variable " + variable + " == " + value;
                case LuaConditionMode.LessThan:
                    return "Lua variable " + variable + " < " + value;
                case LuaConditionMode.LessOrEqualTo:
                    return "Lua variable " + variable + " <= " + value;
                case LuaConditionMode.GreaterThan:
                    return "Lua variable " + variable + " > " + value;
                case LuaConditionMode.GreaterOrEqualTo:
                    return "Lua variable " + variable + " >= " + value;
            }
        }

    }

}
