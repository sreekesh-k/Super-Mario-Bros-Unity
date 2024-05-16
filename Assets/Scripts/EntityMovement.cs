using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    public float speed;
    public Vector2 direction = Vector2.left;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled= false;
    }
    private void OnEnable()
    {
        rigidBody.WakeUp();
    }
    private void OnDisable()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.Sleep();
    }
    public void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y* Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position+velocity*Time.fixedDeltaTime);
        if(rigidBody.rayCast(direction)){
            direction = -direction;
        }
        if (rigidBody.rayCast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0);  
        }
    }

}
