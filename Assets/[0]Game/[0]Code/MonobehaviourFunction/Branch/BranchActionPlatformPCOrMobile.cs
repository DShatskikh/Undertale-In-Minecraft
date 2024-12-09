using RimuruDev;

namespace Game
{
    public class BranchActionPlatformPCOrMobile : BranchActionBase
    {
        public override bool IsTrue()
        {
            return GameData.DeviceType == CurrentDeviceType.PC;
        }
    }
}