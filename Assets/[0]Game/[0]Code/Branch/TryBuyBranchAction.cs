using UnityEngine;

namespace Game
{
    public class TryBuyBranchAction : BranchActionBase
    {
        [SerializeField]
        private int _amount;
        
        public override bool IsTrue()
        {
            var money = GameData.Saver.LoadKey(GameData.MoneyKey);

            if (money < _amount)
                return false;
            
            money -= _amount;
            GameData.Saver.Save(GameData.MoneyKey, money);
            EventBus.OnMoneyUpgrade(money);
            return true;
        }
    }
}