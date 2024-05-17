using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Sprite deadSprite;
    public SpriteRenderer spriteRenderer;

    private void Reset()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
    }
    private void OnEnable()
    {
        UpdateSprite();
        DissablePhysics();
        StartCoroutine(AnimateSprite());
    }
    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;

        if(deadSprite != null )
        {
            spriteRenderer.sprite = deadSprite;
        }
    }
    private void DissablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if(playerMovement != null)
        {
            playerMovement.enabled = false;
            GetComponentInChildren<AnimationSprites>().enabled = false; 
        }
        EntityMovement e = GetComponent<EntityMovement>(); 
        if(e != null)
        {
            e.enabled = false;
        }

    }
    private IEnumerator AnimateSprite()
    {
        float elapsed = 0f;
        float duration = 3f;
        float jump = 10f;
        float gravity = -36f;
        Vector3 velocity = Vector3.up * jump;

        while(elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
