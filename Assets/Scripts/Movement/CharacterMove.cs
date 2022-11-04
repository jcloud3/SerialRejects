using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

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
    [SerializeField] private LayerMask wallLayer;

    [Header("Jumping Character")]
    [SerializeField] private Rigidbody2D charRB;
    [SerializeField] private float jumpVal = 10f;
    [SerializeField] private int possibleJumps = 1;
    [SerializeField] private int currentJumps = 0;
    [SerializeField] private bool onBase = false;
    [SerializeField] private Transform jumpDetector;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private Transform rightDetector;
    [SerializeField] private Transform leftDetector;
    [SerializeField] private Transform ceilingDetector;
    [SerializeField] private float wallDetectionDistance;
    [SerializeField] private float jumpingGravityScale;
    [SerializeField] private float fallingGravityScale;
    [SerializeField] private Animator m_animator;
    private GameObject model;
    private bool jump;

    private bool attack;
    private bool facingRight = true;
    private Vector2 movementInput = Vector2.zero;

    
    
    CharacterController input;
    //Controls controls = new Controls();
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        //model = transform.find("Character Model");
        input = GetComponent<CharacterController>();
        //m_animator = Child.GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
        DetectWall();
    }
    public void OnJump(InputAction.CallbackContext context){
        jump = context.performed;
        //jump = context.ReadValue<float>()>0;
        jump = context.action.triggered && currentJumps < possibleJumps;
        Debug.Log("jump");
        
    }

    public void OnAttack(InputAction.CallbackContext context){
        //context.ReadValue<float>()>0;
        
        //Debug.Log(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Trix Attack A"));
        if (context.action.triggered){
            m_animator.SetTrigger("AttackA");
            attack = true;
            Debug.Log("Attack");
        }
        
    }
private void Update()
    {
        
        
        /*controls = input.GetInput();
        
            DetectWall();
            
        if (controls.JumpState && currentJumps < possibleJumps)
        {
            jump = true;
        }*/
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
            DetectWall();
            Vector3 targetVelocity = new Vector2(movementInput.x * hSpeed, movementInput.y * vSpeed);
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
            if(onBase){
                m_animator.SetFloat("walking",Mathf.Abs(_velocity.x)+Mathf.Abs(_velocity.y));
            }
            // --- 

            // rotate if we're facing the wrong way
            if (movementInput.x > 0 && !facingRight)
            {
                flip();
            } else if(movementInput.x < 0 && facingRight)
            {
                flip();
            }
        }
        /*if(attack){
            m_animator.SetTrigger("AttackA");
            attack = false;
        }*/
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
    //finish this
    private void DetectWall(){
        RaycastHit2D hit;
        if (movementInput.y>0){
            hit = Physics2D.Raycast(ceilingDetector.position, Vector2.up, wallDetectionDistance, wallLayer);
            if(hit.collider != null)
        {
            //also probably need to adjust movement smoothing so it slams to a stop rather than sliding
            movementInput.y=0;
            velocity.y = 0;
            //baseRB.velocity.y=0;
        }
        }
        else if (movementInput.y<0){
             hit = Physics2D.Raycast(ceilingDetector.position, -Vector2.up, wallDetectionDistance, wallLayer);
            if(hit.collider != null)
        {
            
            movementInput.y=0;
            velocity.y = 0;
            //baseRB.velocity.y=0;
        }
        }
        if (movementInput.x<0){
            if (facingRight){
             hit = Physics2D.Raycast(leftDetector.position, Vector2.left, wallDetectionDistance, wallLayer);
            }
            else{
                 hit = Physics2D.Raycast(rightDetector.position, Vector2.left, wallDetectionDistance, wallLayer);
            }
            if(hit.collider != null)
        {
            //Debug.Log("left");
            movementInput.x=0;
            velocity.x=0;
            //baseRB.velocity.x=0;
        }
        }
        else if (movementInput.x>0){
            if(facingRight){
             hit = Physics2D.Raycast(rightDetector.position, Vector2.right, wallDetectionDistance, wallLayer);
            }
            else{
                 hit = Physics2D.Raycast(leftDetector.position, Vector2.right, wallDetectionDistance, wallLayer);
            }
            if(hit.collider != null)
        {
            
            movementInput.x=0;
            velocity.x=0;
            //baseRB.velocity.x=0;
        }
        }
        
        //return(movementInput.x!=0 && movementInput.y!=0);
    }

}
