using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyMove : MonoBehaviour
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

    [SerializeField] private GameObject[] players;
    private GameObject model;
    private bool jump;

    private bool attack;
    private bool facingRight = false;
    private Vector2 movementInput = Vector2.zero;

    private GameObject target;

    

    private float distanceFromTarget;

    
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        charRB.gravityScale = 0;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

private void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        /*
        movementInput.x = 0;
        movementInput.y = 0;
        FindTarget();
        Move();
        */
    }
    public void Move()
    {
        if (!onBase && doesCharacterJump)
        {
            detectBase();
        }
        if (canMove){
            FindTarget();
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
                    //this.attachedRigidbody.useGravity = false;
                    charRB.velocity = _velocity;
                    charRB.gravityScale = 0;
                    
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
            else{
                charRB.velocity = new Vector2(_velocity.x, charRB.velocity.y);
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
        
    }
    public void MoveAway()
    {
        if (!onBase && doesCharacterJump)
        {
            detectBase();
        }
        if (canMove){
            AvoidTarget();
            
            
            //int moveAdjustX = Random.Range(-1,2);
            //int moveAdjustY = Random.Range(-1,2);
            movementInput.y *= -1;
            DetectWall();
            
            Vector3 targetVelocity = new Vector2(movementInput.x * hSpeed , movementInput.y * vSpeed );
            Vector2 _velocity = Vector3.SmoothDamp(baseRB.velocity, targetVelocity, ref velocity, movementSmoothing);
            baseRB.velocity = _velocity;
            
            
            //check if jumping
            if (doesCharacterJump)
            {
                if (onBase)
                {
                    // on base
                    //this.attachedRigidbody.useGravity = false;
                    charRB.velocity = _velocity;
                    charRB.gravityScale = 0;
                    
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
            else{
                charRB.velocity = new Vector2(_velocity.x, charRB.velocity.y);
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
        
    }
        
        public GameObject FindTarget(){
            NearestTarget();
            distanceFromTarget = Vector2.Distance(target.transform.position,this.transform.position);
            if (distanceFromTarget>3.5f){
                movementInput.x = (target.transform.position.x - this.transform.position.x);
                movementInput.y = (target.transform.position.y - this.transform.position.y);
                
            }
            else{
                if(Mathf.Abs(target.transform.position.y - this.transform.position.y) > 0.2f){
                    movementInput.x = 0;
                    movementInput.y = (target.transform.position.y - this.transform.position.y);
                }
                else{
                    movementInput.x = (target.transform.position.x - this.transform.position.x);
                    movementInput.y = (target.transform.position.y - this.transform.position.y);
                }
            }
            movementInput.Normalize();
            return target;
        }
        public void AvoidTarget(){
            NearestTarget();
            distanceFromTarget = Vector2.Distance(target.transform.position,this.transform.position);
            
            movementInput.x = -1*(target.transform.position.x - this.transform.position.x);
            movementInput.y = -1*(target.transform.position.y - this.transform.position.y);
                
            
            movementInput.Normalize();
            
        }
    public void NearestTarget(){
        if (target == null){
            float nearest = 1000;
            foreach (GameObject player in players){
                
                if (Vector2.Distance(player.transform.position,this.transform.position)<nearest){
                    nearest = Vector2.Distance(player.transform.position,this.transform.position);
                    target = player;
                }
                Debug.Log(target);
            }
        }
    }
    public void flip(){
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
    private void detectBase()
    {

        RaycastHit2D hit = Physics2D.Raycast(jumpDetector.position, -Vector2.up, detectionDistance, detectLayer);
        if(hit != null)
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
    
    private void DetectWall(){
        RaycastHit2D hit;
        if (movementInput.y>0){
            hit = Physics2D.Raycast(ceilingDetector.position, Vector2.up, wallDetectionDistance, wallLayer);
            if(hit.collider != null)
        {
            
            movementInput.y=0;
            velocity.y = 0;
        }
        }
        else if (movementInput.y<0){
             hit = Physics2D.Raycast(ceilingDetector.position, -Vector2.up, wallDetectionDistance, wallLayer);
            if(hit.collider != null)
        {
            
            movementInput.y=0;
            velocity.y = 0;
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
            movementInput.x=0;
            velocity.x=0;
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
        }
        }
        
    }


    private void Attack(){}

public float GetDistanceToTarget(){
    return distanceFromTarget;
}
public void SetJump(){
    jump = true;
}
    
    
}
