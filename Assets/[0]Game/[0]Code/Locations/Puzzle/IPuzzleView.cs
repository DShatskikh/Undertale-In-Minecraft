namespace Game
{
    public interface IPuzzleView
    {
        void SelectSlot(BaseSlotController baseSlotController);
        void OnSubmitDown();
        void OnSubmitUp();
    }
}