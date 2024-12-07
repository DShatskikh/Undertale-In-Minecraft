using System.Collections;

namespace Game
{
    public interface IDamageAndDeath
    {
        int Health { get; }
        int MaxHealth { get; }
        IEnumerator AwaitDamage(int damage);
        IEnumerator AwaitDeath();
    }
}