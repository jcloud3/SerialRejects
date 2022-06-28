using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private bool canMove = true;


    [SerializeField] private bool doesCharacterJump = false;
    
    
    [Header("Base/Root")]
    [SerializeField] private Rigidbody2D baseRB;
    [SerializeField] private float hSpeed = 10f;
    [SerializeField] private float vSpeed = 10f;
    [Range(0,1.0f)]
    [SerializeField] float movementSmoothing = 0.5f;

    [Header("Jumping Character")]
    [SerializeField] private Rigidbody2D charRB;
    [SerializeField] private float jumpVal = 10f;
    [SerializeField] private int possibleJumps = 1;
    [SerializeField] private int currentJumps = 0;
    [SerializeField] private bool onBase = false;
    [SerializeField] private Transform jumpDetector;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private float jumpingGravityScale;
    [SerializeField] private float fallingGravityScale;
    private bool jump;
    private bool facingRight = true;

    PlayerInput input;
    Controls controls = new Controls();
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        
    }
private void Update()
    {
        controls = input.GetInput();
        if (controls.JumpState && currentJumps < possibleJumps)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if (!onBase && doesCharacterJump)
        {
            detectBase();
        }
        if (canMove){
            Vector3 targetVelocity = new Vector2(controls.HorizontalMove * hSpeed, controls.VerticalMove * vSpeed);
            Vector2 _velocity = Vector3.SmoothDamp(baseRB.velocity, targetVelocity, ref velocity, movementSmoothing);
            baseRB.velocity = _velocity;
            
            //check if jumping
            if (doesCharacterJump)
            {
                if (onBase)
                {
                    // on base
                    charRB.velocity = _velocity;
                }
                else
                {
                    // in air
                    if (charRB.velocity.y < 0)
                    {
                        charRB.gravityScale = fallingGravityScale;
                    }

                    charRB.velocity = new Vector2(_velocity.x, charRB.velocity.y);
                }

                if (jump)
                {
                    charRB.AddForce(Vector2.up * jumpVal, ForceMode2D.Impulse);
                    charRB.gravityScale = jumpingGravityScale;
                    jump = false;
                    currentJumps++;
                    onBase = false;
                }
            }
            // --- 

            // rotate if we're facing the wrong way
            if (controls.HorizontalMove > 0 && !facingRight)
            {
                flip();
            } else if(controls.HorizontalMove < 0 && facingRight)
            {
                flip();
            }
        }
    }
        
    
    private void flip(){
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
    private void detectBase()
    {

        RaycastHit2D hit = Physics2D.Raycast(jumpDetector.position, -Vector2.up, detectionDistance, detectLayer);
        if(hit.collider != null)
        {
            onBase = true;
            currentJumps = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (doesCharacterJump)
        {
            Gizmos.DrawRay(jumpDetector.transform.position, -Vector3.up * detectionDistance);
        }
    }

}
