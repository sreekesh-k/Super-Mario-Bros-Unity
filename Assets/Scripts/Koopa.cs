using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite EnterShell;
    public float shellSpeed = 12f;
    private bool shelled;
    private bool shellMoving;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.down)) {
                Entershell();
            }  
            else{
                collision.gameObject.GetComponent<Player>().hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!shellMoving)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                HitShell(direction);
            }
            else
            {
                other.gameObject.GetComponent<Player>().hit();
            }
        }
    }
    private void Entershell()
    {
        shelled = true;

        Destroy(GetComponent<AnimationSprites>());
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = EnterShell;

    }
    
    private void HitShell(Vector2 direction)
    {
        shellMoving = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void OnBecameInvisible()
    {
        if (shelled) {
            Destroy(gameObject);
        }
    }

}
