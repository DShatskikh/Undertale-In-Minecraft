using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public sealed class PalesosPortal : UseObject
    {
        [SerializeField]
        private LocalizedString _monolog_1; // От этого портала идет зловещая аура
        
        [SerializeField]
        private LocalizedString _select_1; // Войти?

        [SerializeField]
        private LocalizedString _monolog_3_no; // Вам слишком страшно входить

        [SerializeField]
        private LocalizedString[] _monologues_2_yes; // Вы входите
                                            // Вы чувствуете сильный запах пыли
                                            // По какой-то причине вам хочется смеяться и веселиться вечность забыв обо всём
        
       [SerializeField] 
       private Replica _dialog_4; // Чувааак!                                

       [SerializeField]
       private GameObject _location;

       [SerializeField]
       private ExitLocation _exitLocation;
       
       // Далее экран становится белым и курица попадает в пылесосный мир
       
       public override void Use()
       {
           StartCoroutine(Await());
       }

       private IEnumerator Await()
       {
           GameData.Monolog.Show(new[] {_monolog_1});
           yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
           GameData.Select.Show(_select_1, EnterPortal, () =>
           {
               GameData.Monolog.Show(new[] {_monolog_3_no});
           });
       }

       private void EnterPortal()
       {
           StartCoroutine(AwaitEnterPortal());
       }

       private IEnumerator AwaitEnterPortal()
       {
           GameData.Monolog.Show(_monologues_2_yes);
           yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
           _exitLocation.Exit();
       }
    }
}