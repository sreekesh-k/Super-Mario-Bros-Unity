using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float moveSpeed = 8f;
    private float inputAxis;
    private Vector2 velocity;
    private Camera cam;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime/2f) ,2); 

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        cam= Camera.main;
    }
    private void Update()
    {
        HorizontalMovements();
        grounded = rigidBody.rayCast(Vector2.down);
        if(grounded)
        {
            groundedMovement();
        }
        applyGravity();
        
    }
    private void groundedMovement()
    {
        jumping = velocity.y > 0f;
        velocity.y = Mathf.Max(velocity.y, 0f);
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }
    private void applyGravity()
    {
        bool falling =velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling? 2f : 1f;
        velocity.y += gravity * multiplier *  Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y,gravity/2f);
    }
    private void HorizontalMovements()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x,moveSpeed*inputAxis,moveSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidBody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + .5f, rightEdge.x-.5f);

        rigidBody.MovePosition(position);

    }
}
