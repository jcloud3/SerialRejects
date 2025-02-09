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
    
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private Transform rightDetector;
    [SerializeField] private Transform leftDetector;
    [SerializeField] private Transform ceilingDetector;
    [SerializeField] private float wallDetectionDistance;
    
    [SerializeField] private Animator m_animator;
    private GameObject model;
    private bool jump;
    private int dashCount = 0;
    private int maxDash = 1;
    private int dashFrames = 0;
    private int maxDashFrames = 55;
    private bool attack;
    private bool facingRight = true;
    private Vector2 movementInput = Vector2.zero;

    [SerializeField] HealthBarAdjust _healthBar;
    
    
    private Player playerInfo;

    private Vector3 velocity = Vector3.zero;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        //model = transform.find("Character Model");
        
        //m_animator = Child.GetComponent<Animator>();
        charRB.gravityScale = 0;
        playerInfo = GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

    }
    public void OnSpawn(InputAction.CallbackContext context){
        
        
        gameManager.SpawnPlayer("trix");
        m_animator.SetTrigger("Spawn");
        Debug.Log("Spawn");
        
    }
    
    public void OnHeal(InputAction.CallbackContext context){
        
        //jump = context.ReadValue<float>()>0;
        PlayerHeal(10);
        Debug.Log("heal");
        
    }

    public void OnDamage(InputAction.CallbackContext context){
        
        //jump = context.ReadValue<float>()>0;
        PlayerTakeDamage(10);
        Debug.Log("damage");
        if (playerInfo.GetHealth()<=0){
            m_animator.SetBool("Death",true);
        }
        
    }

    public void OnDash(InputAction.CallbackContext context){
        if (dashCount<maxDash){
            dashCount++;
            m_animator.SetBool("Dash",true);
        }
        

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
        //Need to change this to use delta time rather than testing current animation state.
        if (context.action.triggered){
            if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Trix_Attack_A")){
                m_animator.SetTrigger("AttackB");
                Debug.Log("AttackB");
            }
            else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Trix_Attack_B")){
                Debug.Log("AttackC");
                m_animator.SetTrigger("AttackC");
            }
            else if(!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Trix_Attack_C")){
                m_animator.SetTrigger("AttackA");
                //Debug.Log("AttackA");
            }
            
            attack = true;
            
        }
        
    }

    private void PlayerTakeDamage(int damage){
        playerInfo.DamageUnit(damage);
       
    }
    //call this to heal player
    private void PlayerHeal(int healAmount){
        playerInfo.HealUnit(healAmount);
        
    }
private void Update()

    {
        
        
    }

    private void FixedUpdate()
    {
        
        Move();
    }
    public void Move()
    {
        
        if (canMove){
            DetectWall();
            Vector3 targetVelocity = new Vector2(movementInput.x * hSpeed, movementInput.y * vSpeed);
            Vector2 _velocity = Vector3.SmoothDamp(baseRB.velocity, targetVelocity, ref velocity, movementSmoothing);
            baseRB.velocity = _velocity;
            
            
            //check if jumping
            if (doesCharacterJump)
            {
                
                    // on base
                    charRB.gravityScale = 0;
                    charRB.velocity = _velocity;
                    if (dashCount>0){
                        //need to lockout any changes in velocity until dash ends
                        dashFrames++;
                        if (dashFrames<maxDashFrames){
                            charRB.velocity = new Vector2(charRB.velocity.x * 1.5f,charRB.velocity.y * 1.5f);
                            baseRB.velocity = charRB.velocity;
                        }
                        else{
                            dashFrames = 0;
                            dashCount = 0;
                            m_animator.SetBool("Dash",false);
                        }
                    }
                
                

                if (jump)
                {
                    
                    charRB.AddForce(Vector2.up * jumpVal, ForceMode2D.Impulse);
                    
                    jump = false;
                    currentJumps++;
                    //onBase = false;
                }
            }
            
                m_animator.SetFloat("walking",Mathf.Abs(_velocity.x)+Mathf.Abs(_velocity.y));
            
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
