using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
 
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
    }

}
