using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float moveSpeed = 8f;
    private float inputAxis;
    private Vector2 velocity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

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
        position += velocity * Time.deltaTime;
        rigidBody.MovePosition(position);

    }
}
