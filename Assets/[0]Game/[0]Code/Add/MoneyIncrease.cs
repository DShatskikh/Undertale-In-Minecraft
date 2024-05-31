namespace Game
{
    public class MoneyIncrease : IncreaseInt
    {
        public override void Use()
        {
            base.Use();
            EventBus.OnMoneyUpgrade.Invoke(GameData.Saver.LoadKey(GameData.MoneyKey));
        }
    }
}