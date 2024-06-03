using UnityEngine;

namespace PixelCrushers.QuestMachine
{

    /// <summary>
    /// This class helps Quest Machine's demo work with the New Input System. It
    /// registers the inputs defined in DemoInputActions for use with the 
    /// Input Device Manager.
    /// </summary>
    public class DemoInputRegistration : MonoBehaviour
    {

#if USE_NEW_INPUT

        private DemoInputActions actions;

        // Track which instance of this script registered the inputs, to prevent
        // another instance from accidentally unregistering them.
        protected static bool isRegistered = false;
        private bool didIRegister = false;

        void Awake()
        {
            actions = new DemoInputActions();
        }

        void OnEnable()
        {
            if (!isRegistered)
            {
                isRegistered = true;
                didIRegister = true;
                actions.Enable();
                InputDeviceManager.RegisterInputAction("Horizontal", actions.Player.Horizontal);
                InputDeviceManager.RegisterInputAction("Vertical", actions.Player.Vertical);
                InputDeviceManager.RegisterInputAction("Fire1", actions.Player.Fire1);
                InputDeviceManager.RegisterInputAction("Fire2", actions.Player.Fire2);
            }
        }

        void OnDisable()
        {
            if (didIRegister)
            {
                isRegistered = false;
                didIRegister = false;
                actions.Disable();
                InputDeviceManager.UnregisterInputAction("Horizontal");
                InputDeviceManager.UnregisterInputAction("Vertical");
                InputDeviceManager.UnregisterInputAction("Fire1");
                InputDeviceManager.UnregisterInputAction("Fire2");
            }
        }

#endif

    }
}
