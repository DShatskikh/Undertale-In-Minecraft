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
        private GameObject _blackScreen;
        
        [SerializeField]
        private LocalizedString[] _monologues_2_yes; // Вы входите
                                            // Вы чувствуете сильный запах пыли
                                            // По какой-то причине вам хочется смеяться и веселиться вечность забыв обо всём
        
       [SerializeField] 
       private Replica _dialog_4; // Чувааак!                                
        
       // Далее экран становится белым и курица попадает в пылесосный мир
       
       public override void Use()
       {
            
       }
    }
}