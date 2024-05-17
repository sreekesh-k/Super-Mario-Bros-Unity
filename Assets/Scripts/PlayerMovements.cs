using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;
    private Vector2 velocity;
    private float inputAxis;
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25 || Mathf.Abs(inputAxis)>0.25;
    public bool sliding => (inputAxis > 0 && velocity.x <0)||(inputAxis < 0 && velocity.x > 0);

    [Header("Physics")]
    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        collider.enabled = true;
        velocity = Vector2.zero;
    }

    private void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.rayCast(Vector2.down);

        if (grounded)
        {
            GroundedMovement();
        }
        else
        {
            AirborneMovement();
        }

        ApplyGravity();
    }

    private void FixedUpdate()
    {
        Vector3 leftEdge = camera.ScreenToWorldPoint(Vector3.zero);
        Vector3 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        // accelerate / decelerate
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        // check if running into a wall
        if (rigidbody.rayCast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        // flip sprite to face direction
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        // prevent gravity from infinitly building up
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        // perform jump
        if (Input.GetButtonDown("Jump"))
        {
            // calculate jump velocity
            float timeToApex = maxJumpTime / 2f;
            velocity.y = (2f * maxJumpHeight) / timeToApex;
            jumping = true;
        }
    }

    private void AirborneMovement()
    {
        // check if bonked head
        if (velocity.y > 0f && rigidbody.rayCast(Vector2.up))
        {
            velocity.y = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goomba"))
        {
            collision.gameObject.GetComponent<Goomba>().Flat();
            velocity.y = 10f;
            jumping = true;
        }
       
    }

    private void ApplyGravity()
    {
        // calculate gravity
        float timeToApex = maxJumpTime / 2f;
        float gravity = (-2f * maxJumpHeight) / Mathf.Pow(timeToApex, 2f);

        // check if falling
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        // apply gravity
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

}
