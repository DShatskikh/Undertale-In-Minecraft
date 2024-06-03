// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

namespace PixelCrushers.QuestMachine
{

    /// <summary>
    /// Manages Unity UI content that has been instantiated from templates.
    /// </summary>
    public class UnityUIInstancedContentManager
    {

        protected List<UnityUIContentTemplate> instances = new List<UnityUIContentTemplate>();

        public List<UnityUIContentTemplate> instancedContent { get { return instances; } }

        protected List<UnityUIContentTemplate> retiredInstances = new List<UnityUIContentTemplate>();

        public void Clear()
        {
            ClearList(instances);
        }

        protected void ClearList(List<UnityUIContentTemplate> list)
        { 
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Despawn();
            }
            list.Clear();
        }

        public void Add(UnityUIContentTemplate instance, RectTransform container)
        {
            if (container == null)
            {
                Debug.LogError("Quest Machine: Container isn't assigned to hold instance of UI template.", instance);
                return;
            }
            instance.gameObject.SetActive(true);
            instances.Add(instance);
            instance.transform.SetParent(container, false);
        }

        public void Remove(UnityUIContentTemplate instance)
        {
            instances.Remove(instance);
            instance.Despawn();
        }

        public UnityUIContentTemplate GetLastAdded()
        {
            return (instances.Count > 0) ? instances[instances.Count - 1] : null;
        }

        /// <summary>
        /// Moves all instanced content to the retiredInstances list.
        /// </summary>
        public void RetireInstances()
        {
            retiredInstances.Clear();
            retiredInstances.AddRange(instancedContent);
            instancedContent.Clear();
        }

        public void DespawnRetiredInstances()
        {
            ClearList(retiredInstances);
        }

        public UnityUITextTemplate AcquireTextInstance(UnityUITextTemplate template, string text)
        {
            var existingInstance = retiredInstances.Find(x => x is UnityUITextTemplate && (x as UnityUITextTemplate).Matches(text));
            if (existingInstance != null)
            {
                retiredInstances.Remove(existingInstance);
                existingInstance.transform.SetAsLastSibling();
                return existingInstance as UnityUITextTemplate;
            }
            else
            {
                var newTextInstance = GameObject.Instantiate<UnityUITextTemplate>(template);
                newTextInstance.Assign(text);
                return newTextInstance;
            }
        }
    }
}
