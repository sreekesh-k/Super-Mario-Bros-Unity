using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flat;

    public void Flat()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimationSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flat;
        Destroy(gameObject,0.5f);
    }
    
}
