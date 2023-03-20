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
    public bool isHanging;
    
    
    
    [HideInInspector] public Rigidbody2D rb;
    private Animator _animator;
    [HideInInspector] public float Horizontal;
    [HideInInspector] public bool isGrounded;
    private bool _isJumping;
    private float _jumpTime;
    private float _coyoteTimeLeft;
    public bool freeze;
    public bool isFacingRight;

    public float momentumX;
    public float wallJumpMomentumX;
    
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        groundCheckGO = GameObject.Find("GroundDistance");
    }

    void Update()
    {
        //If climbing freeze player
        if (!freeze)
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
        
        //player facing
        if(Horizontal > 0.001) {
            isFacingRight = true;
        }
        if(Horizontal < -0.001) {
            isFacingRight = false;
        }

        if(isFacingRight) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        //Ledge climb


        RaycastHit2D distanceToGround = Physics2D.Raycast(this.groundCheckGO.transform.position, -Vector2.up);
        if (ledgeDetected && !wallDetected && distanceToGround.distance > 0.75f && !isClimbing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("climbing");
                isHanging = true;
                freezePlayer(true);

                //Position player
                RaycastHit2D hit = Physics2D.Raycast(transform.position + climboffset, -Vector2.up);
                transform.position = hit.point - new Vector2(1, 0.5f);
            }
        }
        if(Input.GetButtonUp("Jump") && isHanging && !isClimbing ) {
            isHanging = false;
            freezePlayer(false);
        }

        if(isFacingRight && Input.GetKeyDown(KeyCode.A) && isHanging && !isClimbing) {
            Debug.Log("Edge jump to the Right");
            isHanging = false;

            isClimbing = false;
            freezePlayer(false);

            wallJumpMomentumX = -6;
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }
        if(!isFacingRight && Input.GetKeyDown(KeyCode.D) && isHanging && !isClimbing) {
            Debug.Log("Edge jump to the Left");
            isHanging = false;

            isClimbing = false;
            freezePlayer(false);

            wallJumpMomentumX = 6;
            rb.velocity = new Vector2(rb.velocity.x, 5);

        }

        if(Input.GetKeyDown(KeyCode.W) && isHanging) {
            Debug.Log("edge climb");


            StartCoroutine(ledgeClimb());
        }
        

        if(wallJumpMomentumX != 0) {
            wallJumpMomentumX = Mathf.Lerp(wallJumpMomentumX, 0, 2f * Time.deltaTime);
            if(isFacingRight && wallJumpMomentumX > -0.15)
                wallJumpMomentumX = 0;
            if(!isFacingRight && wallJumpMomentumX > 0.15)
                wallJumpMomentumX = 0;
        }
    }

    void FixedUpdate()
    {
        if (!freeze)
        {
            

            rb.gravityScale = 1.15f;
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            momentumX = Horizontal * maxSpeed + wallJumpMomentumX;


            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }

            rb.velocity = new Vector2(momentumX, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
        
    }



    public void freezePlayer(bool State) {
        freeze = State;
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
        isHanging = false;
        freezePlayer(false);
    }
}