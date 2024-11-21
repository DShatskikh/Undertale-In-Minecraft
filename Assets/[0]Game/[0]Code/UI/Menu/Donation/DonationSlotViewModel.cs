using UnityEngine.Serialization;
using YG.Utils.Pay;

namespace Game
{
    public class DonationSlotViewModel : BaseSlotController
    {
        public Purchase Model;
        private DonationSlotView _view;
        private DonationScreen _screen;

        public void Init(DonationScreen screen, Purchase model)
        {
            _screen = screen;
            Model = model;
            _view = GetComponent<DonationSlotView>();
            
            _view.Init(Model, this);
        }
        
        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect);
        }

        public override void Select()
        {
            _screen.SelectSlot(this);
        }

        public override void Use()
        {
            _screen.OnSubmitUp();
        }

        public override void SubmitDown()
        {
            _view.SubmitDown();
        }

        public override void SubmitUp()
        {
            _view.SubmitUp();
        }
    }
}