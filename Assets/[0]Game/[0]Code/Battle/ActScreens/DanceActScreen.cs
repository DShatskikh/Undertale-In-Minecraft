namespace Game
{
    public class DanceActScreen : UIPanelBase
    {
        public void Init(DanceActConfig config)
        {
            Activate(true);
            //StartCoroutine(AwaitActive());
        }

        public override void OnSubmitDown()
        {
            throw new System.NotImplementedException();
        }

        public override void OnSubmitUp()
        {
            throw new System.NotImplementedException();
        }

        public override void OnCancel()
        {
            throw new System.NotImplementedException();
        }
    }
}