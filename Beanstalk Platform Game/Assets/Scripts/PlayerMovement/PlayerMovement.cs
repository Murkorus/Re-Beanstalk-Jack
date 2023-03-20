using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Move settings")]
    public float speed = 5f;
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    
    [Header("Jump settings")]
    public float jumpForce = 10f;
    public float maxJumpTime = 0.5f;
    public float coyoteTime = 0.1f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    
    [Header("Ground check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Ledge settings")]
    [SerializeField] public bool ledgeDetected;
    [SerializeField] public bool wallDetected;
    [SerializeField] public bool isClimbing;
    [SerializeField] private Vector3 climboffset;
    private GameObject groundCheckGO;
    
    
    
    [HideInInspector] public Rigidbody2D rb;
    private Animator _animator;
    [HideInInspector] public float Horizontal;
    [HideInInspector] public bool isGrounded;
    private bool _isJumping;
    private float _jumpTime;
    private float _coyoteTimeLeft;
    
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        groundCheckGO = GameObject.Find("GroundDistance");
    }

    void Update()
    {
        //If climbing freeze player
        if (!isClimbing)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && (isGrounded || _coyoteTimeLeft > 0))
            {
                _isJumping = true;
                _jumpTime = maxJumpTime;
            }
            else if (Input.GetButton("Jump") && _isJumping)
            {
                if (_jumpTime > 0)
                {
                    _coyoteTimeLeft = 0;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    _jumpTime -= Time.deltaTime;
                }
                else
                {
                    _isJumping = false;
                }
            }
            else
            {
                _isJumping = false;
            }

            _coyoteTimeLeft = isGrounded ? coyoteTime : _coyoteTimeLeft - Time.deltaTime;
        }
        
        //Ledge climb
        Debug.Log(ledgeDetected);

        RaycastHit2D distanceToGround = Physics2D.Raycast(this.groundCheckGO.transform.position, -Vector2.up);
        if (ledgeDetected && !wallDetected && distanceToGround.distance > 0.05f && !isClimbing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine(ledgeClimb());
                Debug.Log("climbing");
            }
        }
        
    }

    void FixedUpdate()
    {
        if (!isClimbing)
        {
            rb.gravityScale = 1.15f;
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            float targetVelocity = Horizontal * maxSpeed;
            float moveSpeed = Mathf.MoveTowards(rb.velocity.x, targetVelocity, acceleration * Time.fixedDeltaTime);
        
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            
        }
        
    }




    IEnumerator ledgeClimb()
    {

        isClimbing = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + climboffset, -Vector2.up);
        GameObject.Find("CameraLook").transform.position = hit.point;
        transform.position = hit.point - new Vector2(1, 0.5f);

        yield return new WaitForSeconds(0.75f);
        
        GameObject.Find("CameraLook").transform.localPosition = new Vector3(0, 0, 0);
        transform.position = hit.point;
        isClimbing = false;
    }
}