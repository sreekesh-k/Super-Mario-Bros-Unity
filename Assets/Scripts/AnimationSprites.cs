using UnityEngine;

public class AnimationSprites : MonoBehaviour
{
    public Sprite[] sprites;
    public float rate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSp), rate, rate);   
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void AnimateSp()
    {
        frame++;
        if(frame >= sprites.Length) {
            frame = 0;
        }
        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];

        }
    }

}
