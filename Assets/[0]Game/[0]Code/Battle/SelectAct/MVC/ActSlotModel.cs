namespace Game
{
    public class ActSlotModel
    {
        public bool IsSelected;
        public bool IsSelectedOnce;
        public readonly BaseActConfig Act;
        
        public ActSlotModel(BaseActConfig act)
        {
            Act = act;
        }
    }
}