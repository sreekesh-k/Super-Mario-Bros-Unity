using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float moveSpeed = 8f;
    private float inputAxis;
    private Vector2 velocity;
    private Camera cam;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        cam= Camera.main;
    }
    private void Update()
    {
        HorizontalMovements();
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
