using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flat;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().starpower)
            {
                Hit();
            }
            else if(collision.transform.DotTest(transform,Vector2.down)){
                Flat();
            }
            else{
                collision.gameObject.GetComponent<Player>().hit();
            }
            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }
    public void Flat()
    {
        // Collider2D[] colliders = GetComponents<Collider2D>();
        // foreach (Collider2D collider in colliders)
        // {
        //     collider.enabled = false;
        // }
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimationSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flat;
        Destroy(gameObject,0.5f);
    }
    private void Hit()
    {

        GetComponent<AnimationSprites>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
