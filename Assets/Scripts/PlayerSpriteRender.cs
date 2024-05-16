using UnityEngine;

public class PlayerSpriteRender : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Sprite run;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();

    }
    private void Update()
    {
        
    }
    private void LateUpdate()
    {
        
    }
}
