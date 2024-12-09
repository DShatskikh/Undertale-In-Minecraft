using UnityEngine;

namespace Game
{
    public class ExitMenuButton : MenuButton
    {
        private ExitSlotConfig _config;
        
        public void Init(ExitSlotConfig config)
        {
            _config = config;
            StartCoroutine(AwaitLoadText(_config.Name));
        }

        protected override Sprite Icon { get; }
    }
}