namespace Game
{
    public class ActSlotModel
    {
        public bool IsSelected;
        public readonly Act Act;
        
        public ActSlotModel(Act act)
        {
            Act = act;
        }
    }
}