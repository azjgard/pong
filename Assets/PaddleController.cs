using UnityEngine;


public class PaddleController : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField]
    private KeyCode up = KeyCode.W;
    [SerializeField]
    private KeyCode down = KeyCode.S;

    [Header("Movement Values")]
    [SerializeField]
    private float acceleration = 0.0f;
    [SerializeField]
    private float accelerationDirectionChange = 0.0f;
    [SerializeField]
    private float maxSpeed = 0.0f;

    private Vector2 startingPosition;

    private Vector2 movementVector => 
        new Vector2(0, acceleration);

    private Vector2 directionChangeVector =>
        new Vector2(0, accelerationDirectionChange);

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement() {
        if (Input.GetKey(up) && rb.velocity.y < maxSpeed) {
            Move(movementVector * Time.deltaTime);
            if (rb.velocity.y < 0) {
                Move(directionChangeVector * Time.deltaTime);
            }
        }
        else if (Input.GetKey(down) && rb.velocity.y > -maxSpeed) {
            Move(-movementVector * Time.deltaTime);
            if (rb.velocity.y > 0) {
                Move(-directionChangeVector * Time.deltaTime);
            }
        }
    }

    private void Move(Vector2 vector) {
        rb.AddForce(
            vector,
            ForceMode2D.Impulse
        );
    }
    
    public void Reset() {
        transform.position = startingPosition;
        ZeroOutVelocity();
    }

    private void ZeroOutVelocity() {
        rb.AddForce(
            new Vector2(
                -rb.velocity.x,
                -rb.velocity.y
            ),
            ForceMode2D.Impulse
        );
    }
}
