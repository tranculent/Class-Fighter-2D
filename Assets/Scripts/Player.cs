using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float crounchingSpeed = .36f;
    [SerializeField] float padding = 1f;
    [SerializeField] GroundCollider groundCollider;

    private CircleCollider2D jumpingColliderPoint;
    //[SerializeField] Animator animator;
    
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private bool isFacingRight;

    private float flipX;

    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        jumpingColliderPoint = GetComponent<CircleCollider2D>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip(flipX);
        if (groundCollider.getisCollidingWithJumperCollider())
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        flipX = deltaX;

        if (Input.GetButtonDown("Jump") && canJump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            transform.position = new Vector2(newXPos, transform.position.y);
        }
        /*else if (Input.GetButton("Crouch") && !isCrouching)
        {
            // 1. Halve the collider
            float currentColliderSizeY = GetComponent<BoxCollider2D>().size.y;
            float currentColliderSizeX = GetComponent<BoxCollider2D>().size.x;
            GetComponent<BoxCollider2D>().size = new Vector2(currentColliderSizeX, currentColliderSizeY / 2f);
            Debug.Log(GetComponent<BoxCollider2D>().size);
            // 2. Change the x velocity by taking into account the crounching speed variable

            var deltaY = Input.GetAxis("Crouch") * Time.deltaTime * crounchingSpeed;
            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
            transform.position = new Vector2(newXPos, newYPos);

            isCrouching = true;
        }*/
        else
        {
            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            transform.position = new Vector2(newXPos, transform.position.y);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    public bool getIsFacingRight() { return isFacingRight; }
}
