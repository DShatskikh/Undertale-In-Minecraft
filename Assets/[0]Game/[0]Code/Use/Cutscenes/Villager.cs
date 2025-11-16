using Game;
using UnityEngine;

public sealed class Villager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        var characterPosition = GameData.Character.transform.position;

        if (Vector2.Distance(characterPosition, transform.position) < 5)
        {
            if (characterPosition.x > transform.position.x)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
            }
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }
}