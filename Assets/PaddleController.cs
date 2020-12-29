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
    private float slowdownRate = 0.0f;
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
        bool upPressed = Input.GetKey(up);
        bool downPressed = Input.GetKey(down);

        if (upPressed && rb.velocity.y < maxSpeed) {
            Move(movementVector * Time.deltaTime);
            if (rb.velocity.y < 0) {
                Move(directionChangeVector * Time.deltaTime);
            }
        }
        else if (downPressed && rb.velocity.y > -maxSpeed) {
            Move(-movementVector * Time.deltaTime);
            if (rb.velocity.y > 0) {
                Move(-directionChangeVector * Time.deltaTime);
            }
        }

        if (!(upPressed || downPressed) && rb.velocity.y != 0) {
            float slowdownForce = rb.velocity.y * slowdownRate * Time.deltaTime * -10;

            rb.AddForce(
                new Vector2(
                    0,
                    slowdownForce
                ), 
                ForceMode2D.Impulse
            );
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
