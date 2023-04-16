using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


	[Header("Run")]
	public float MaxSpeed; //Target speed we want the player to reach.
	public float Acceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
	public float AccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
	public float Decceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.
	public float DeccelAmount; //Actual force (multiplied with speedDiff) applied to the player .
	public float jumpForce;
	[Space(10)]
	[Range(0.01f, 1)] public float accelInAir; //Multipliers applied to acceleration rate when airborne.
	[Range(0.01f, 1)] public float deccelInAir;
	public bool doConserveMomentum;
	public bool isDodging;
	
	
	public Vector2 moveInput;
	public bool IsFacingRight { get; private set; }
	public float LastOnGroundTime { get; private set; }
	
	[SerializeField] private Transform _groundCheckPoint;
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	[SerializeField] private LayerMask _groundLayer;
	public Rigidbody2D RB { get; private set; }

	[Header("Jump")]
	private bool _isJumping;
	private float _jumpTime;
	public float maxJumpTime;
	
	public bool isGrounded;
	
	[Header("Ledge Settings")]
	public GameObject WallDetection;
	public bool wallDetected;
	public GameObject LedgeDetection;
	public bool ledgeDetected;
	public float ledgeRadius;
	private bool freezePlayer;
	public bool isHanging;
	private bool isClimbing;
	private bool hasPositioned;

	[Header("Ledge offset")]
	//Ray offset
	[SerializeField] private Vector3 _rayOffsetRight;
	[SerializeField] private Vector3 _rayOffsetLeft;

	//Hanging offset;
	[SerializeField] private Vector3 _hangingOffset;
	[SerializeField] private Vector2 _ledgeJump;
	private GameObject groundCheckGO;
	

    private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		IsFacingRight = true;
		groundCheckGO = GameObject.Find("GroundDistance");
	}

	private void Update()
	{
		LastOnGroundTime -= Time.deltaTime;

		if(!freezePlayer || !isDodging)
        	moveInput.x = Input.GetAxisRaw("Horizontal");

		if (moveInput.x != 0 && !isHanging)
			CheckDirectionToFace(moveInput.x > 0);
		
		//Ground Check
		if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
		{
			LastOnGroundTime = 0.1f;
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}

		//Jump
		if(Input.GetButtonDown("Jump") && isGrounded) {
			RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y + jumpForce);
		}

		//Ledge detection
		if(Physics2D.OverlapBox(WallDetection.transform.position, WallDetection.transform.localScale, 0, _groundLayer)) {
			wallDetected = true;
		} else {
			wallDetected = false;
		}

		if(Physics2D.OverlapCircle(LedgeDetection.transform.position, ledgeRadius, _groundLayer)) {
			ledgeDetected = true;
		} else {
			ledgeDetected = false;
		}

		//distance to ground
		RaycastHit2D distanceToGround = Physics2D.Raycast(this.groundCheckGO.transform.position, -Vector2.up);
        if (ledgeDetected && !wallDetected && distanceToGround.distance > 0.75f && !isClimbing)
        {
            if (Input.GetButton("Jump"))
            {
                //Freeze the player
                isHanging = true;
                Freeze(true);

                //Position player
            }

			if(isHanging) 
			{
				if(!hasPositioned) {
					if(IsFacingRight) {
						RaycastHit2D hit = Physics2D.Raycast(transform.position + _rayOffsetRight, -Vector2.up);
                		transform.position = hit.point - new Vector2(_hangingOffset.x, _hangingOffset.y);
					}
					if(!IsFacingRight) {
						RaycastHit2D hit = Physics2D.Raycast(transform.position + _rayOffsetLeft, -Vector2.up);
                		transform.position = hit.point - new Vector2(-_hangingOffset.x, _hangingOffset.y);
					} 
					hasPositioned = true;
				}
			}

			if(Input.GetButtonUp("Jump")) {
				hasPositioned = false;
			}
        } else {
			isHanging = false;
			hasPositioned = false;
		}

		//When releasing Hang
        if(Input.GetButtonUp("Jump") && isHanging && !isClimbing ) {
            isHanging = false;
            Freeze(false);
			hasPositioned = false;
        }

		//Wall jump to the Left
        if(IsFacingRight && Input.GetKeyDown(KeyCode.A) && isHanging && !isClimbing) {
            Debug.Log("Edge jump to the Right");
            isHanging = false;

            isClimbing = false;
            Freeze(false);
			RB.AddForce(new Vector2(_ledgeJump.x, _ledgeJump.y), ForceMode2D.Force);
			hasPositioned = false;
        }

		//Wall jump to the right
        if(!IsFacingRight && Input.GetKeyDown(KeyCode.D) && isHanging && !isClimbing) {
            Debug.Log("Edge jump to the Left");
            isHanging = false;

            isClimbing = false;
            Freeze(false);
			RB.AddForce(new Vector2(Mathf.Abs(_ledgeJump.x), _ledgeJump.y), ForceMode2D.Force);
			hasPositioned = false;
        }


        if(Input.GetKeyDown(KeyCode.W) && isHanging) {
            Debug.Log("edge climb");

            StartCoroutine(ledgeClimb());
        }
	}

    private void FixedUpdate()
	{
		Run();
	}
    
    private void Run()
	{
		if(!freezePlayer && !isDodging) {
			//Calculate the direction we want to move in and our desired velocity
			float targetSpeed = moveInput.x * MaxSpeed;
			
			float accelRate;
			
			if (LastOnGroundTime > 0)
				accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? AccelAmount : DeccelAmount;
			else
				accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? AccelAmount * accelInAir : DeccelAmount * deccelInAir;

			//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
			if(doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
			{
				accelRate = 0; 
			}

			//Calculate difference between current velocity and desired velocity
			float speedDif = targetSpeed - RB.velocity.x;
			//Calculate force along x-axis to apply to thr player

			float movement = speedDif * accelRate;

			//Convert this to a vector and apply to rigidbody
			RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
		}
	}

    
	public void Freeze(bool freezeState) {
		freezePlayer = freezeState;
		if(freezePlayer) {
			RB.velocity = new Vector2(0, 0);
			RB.gravityScale = 0;
		} else {
			RB.velocity = new Vector2(0, 0);
			RB.gravityScale = 1;
		}
	}

	private void Turn()
	{
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}

    public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != IsFacingRight)
			Turn();
	}


	IEnumerator ledgeClimb()
    {

        isClimbing = true;
		RaycastHit2D rayHit;
		if(IsFacingRight) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position + _rayOffsetRight, -Vector2.up);
			rayHit = hit;
		} else {
			RaycastHit2D hit = Physics2D.Raycast(transform.position + _rayOffsetLeft, -Vector2.up);
			rayHit = hit;
		}
        GameObject.Find("CameraLook").transform.position = rayHit.point;
		if(IsFacingRight)
        	transform.position = rayHit.point - new Vector2(_hangingOffset.x, _hangingOffset.y);
		else
        	transform.position = rayHit.point - new Vector2(-_hangingOffset.x, _hangingOffset.y);


        yield return new WaitForSeconds(0.75f);
        
        GameObject.Find("CameraLook").transform.localPosition = new Vector3(0, 0, 0);
        transform.position = rayHit.point + new Vector2(0, 0.5f);
        isClimbing = false;
        isHanging = false;
        Freeze(false);
    }
}