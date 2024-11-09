using System.Collections;

namespace Game
{
    public class SubmitAnimatedButton : AnimatedButton
    {
        protected override void Enable()
        {
            EventBus.Submit += _view.Down;
            EventBus.SubmitUp += _view.Up;
            EventBus.SubmitUp += _button.onClick.Invoke;

            //StartCoroutine(AwaitCheck());
        }

        protected override void Disable()
        {
            EventBus.Submit -= _view.Down;
            EventBus.SubmitUp -= _view.Up;
            EventBus.SubmitUp -= _button.onClick.Invoke;
        }

        private IEnumerator AwaitCheck()
        {
            while (true)
            {
                yield return null;
                
                if (EventBus.Submit == _view.Down)
                    gameObject.SetActive(false);
                
                print(EventBus.Submit);
            }
        }
    }
}