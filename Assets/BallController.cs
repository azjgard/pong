using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BallController : MonoBehaviour
{
    [SerializeField]
    private LayerMask[] collidableLayers = null;
    private int layerMask;

    [Header("Sounds")]
    public AudioSource ballBounce_sound;
    public AudioSource ballStartedMoving_sound;

    private Sprite ballSprite;
    private System.Random rnd;

    private Vector2 velocity;

    private Vector2 startingScale;

    void Start()
    {
        rnd = new System.Random(DateTime.Now.Millisecond);
        ballSprite = GetComponent<SpriteRenderer>().sprite;
        layerMask = generateLayerMask();

        startingScale = transform.localScale;
    }

    void Update()
    {
        HandleBouncing();
        HandleMovement();
    }

    private void HandleMovement() {
        transform.Translate(velocity * Time.deltaTime);
    }

    private void HandleBouncing() {
        bool didBounce = false;

        if (
            IsColliding(CollisionDirection.Top) ||
            IsColliding(CollisionDirection.Bottom)
        ) {
            velocity.y *= -1;
            didBounce = true;
        }
        else if (
            IsColliding(CollisionDirection.Right) ||
            IsColliding(CollisionDirection.Left) 
        ) {
            velocity.x *= -1;
            didBounce = true;
        }

        if (didBounce) {
            velocity *= 1.05f;
            transform.localScale *= 0.99f;
            if (ballBounce_sound != null) {
                ballBounce_sound.Play();
            }
        }
    }

    private enum CollisionDirection { Top, Right, Bottom, Left }
    private bool IsColliding(CollisionDirection direction) {
        Vector2 y = transform.up;
        float yHeight = ballSprite.bounds.extents.y * transform.localScale.y;

        Vector2 x = transform.right;
        float xWidth = ballSprite.bounds.extents.x * transform.localScale.x;

        switch (direction) {
            case CollisionDirection.Top:
                return _IsColliding(y, yHeight);
            case CollisionDirection.Right:
                return _IsColliding(x, xWidth);
            case CollisionDirection.Bottom:
                return _IsColliding(-y, yHeight);
            case CollisionDirection.Left:
                return _IsColliding(-x, xWidth);
            default:
                throw new Exception("Invalid CollisionDirection passed to IsColliding");
        }
    }

    private bool _IsColliding(
        Vector2 direction, 
        float distance
    ) {
        RaycastHit2D hit = Physics2D.Raycast(
            origin: transform.position,
            direction: direction,
            distance: distance * 1.05f,
            layerMask
        );

        return hit.collider != null;
    }

    private Vector2 GenerateStartingVelocity() {
        float x = rnd.Next(3, 5);
        float y = rnd.Next(2, 4);

        RandomSet<int> directions = new RandomSet<int>(new int[] { -1, 1 });

        float xDir = directions.Next();
        float yDir = directions.Next();

        return new Vector2(x * xDir, y * yDir);
    }

    public void StartMoving() {
        velocity = GenerateStartingVelocity();
        if (ballStartedMoving_sound != null) {
            ballStartedMoving_sound.Play();
        }
    }
    
    public void Reset() {
        velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        transform.localScale = startingScale;
    }

    private int generateLayerMask() {
        int layerMask = 0;

        for (int i = 0; i < collidableLayers.Length; i++) {
            int l = (int) Mathf.Log(collidableLayers[i], 2);
            layerMask = layerMask | (1 << l);
        }

        return layerMask;
    }
}
