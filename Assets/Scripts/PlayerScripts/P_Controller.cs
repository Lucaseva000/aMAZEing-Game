using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // ======================================== //
    //               OLD STUFF                  //
    // ======================================== //
    PlayerStateList pState;
    public static PlayerController Instance;
    [Header("Pause Menu")]
    //public GameObject PausePanel;

    [Header("Death Screen")]
    [SerializeField] public PlayerHealth p;
    public GameObject DeathPanel;
    // ======================================== //
    //               NEW STUFF                  //
    // ======================================== //
    // ---------------------------------------- //
    [Header("Animation")]
    [SerializeField] private Animator animator;
    private string currentAnimation;
    // ---------------------------------------- //
    [Header("Key Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private LayerMask terrain;
    [SerializeField] private GameObject rotationPoint;
    [SerializeField] private TrailRenderer tr;
    private Transform pt;
    private float gravity;
    private bool disableMovement = false;
    private bool disableHorizontalMovement = false;
    private PlayerControler playerControls;
    private Vector2 playerSize;
    private Vector2 playerOffset;
    private Vector2 idleSize;
    // ---------------------------------------- //
    [Header("Movement: Horizontal")]
    [SerializeField] private float speed;
    private float xMovement;
    // ---------------------------------------- //
    [Header("Movement: Vertical")]
    [SerializeField]private float damper;
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyateTime;
    private float coyateTimeCounter;
    [SerializeField] private float jumpBuffer;
    private float jumpBufferCounter;
    bool isGrounded;
    private bool doubleJump;
    // ---------------------------------------- //
    [Header("Movement: Dash")]
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashStrength;
    private bool canDash = true;
    // ---------------------------------------- //
    [Header("Movement: Crouch")]
    [SerializeField] private float crouchHeightPercent = 0.625f;
    [SerializeField] private float crouchSpeedPercent = 0.75f;
    private Vector2 crouchSize;
    private bool wantsToCrouch;
    private bool isCrouching;
    private bool cantUncrouch;
    // ---------------------------------------- //
    [Header("Movement: Wall Slide/Jump")]
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallSlideSizePercent; 
    [SerializeField] private float wallJumpDuration; 
    [SerializeField] private float wallJumpAngle; 
    private bool isWallSliding;
    private Vector2 wallSlideSize;
    private bool wallSlide;
    // ---------------------------------------- //

    // ---------------------------------------- //
    private void Awake() {
        if(Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        playerControls = new PlayerControler();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        pt = GetComponent<Transform>();
        tr.emitting = false;
        gravity = rb.gravityScale;
        playerSize = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
        idleSize = playerSize;
        crouchSize = new Vector2(idleSize.x, idleSize.y * crouchHeightPercent);
        wallSlideSize = new Vector2(idleSize.x * wallSlideSizePercent, idleSize.y);
    }
    void OnEnable(){
        playerControls.Enable();
    }
    void OnDisable(){
        playerControls.Disable();
    }
    void Start(){
        if (dashDuration > dashCooldown) dashCooldown = dashDuration;
        playerControls.Land.Jump.started    += TryToJump;
        playerControls.Land.Jump.canceled   += TryToJump;
        playerControls.Land.Dash.started    += TryToDash;
        playerControls.Land.Crouch.started  += CrouchInput;
        playerControls.Land.Crouch.canceled += CrouchInput;
        playerControls.Land.Attack.started += Attack;
        pState = GetComponent<PlayerStateList>();
        p.setRegenPause(false);
    }
    // ---------------------------------------- //
    // Updates
    // In update get all inputs
    void Update(){
        //PauseCheck();
        DeathCheck();
        xMovement = playerControls.Land.Move.ReadValue<float>();
        Flip();
        rotationPoint.transform.rotation = Quaternion.Euler(0, 0, GetAngleTowardsMouse());
        CoyateTimer();
        if (jumpBufferCounter > 0) jumpBufferCounter -= Time.deltaTime;
        IsWallSliding();
        if (p.getHealth() > p.getMaxHealth())
        {
            p.setHealth(p.getMaxHealth());
        }

    }
    // In fixed update do all physics
    void FixedUpdate(){
        playerOffset = new Vector2(pt.position.x, pt.position.y);
        HorizontalMovement();
        Jump();
        Crouch();
        WallSlide();
        if (!p.getRegen())
        {
            _ = StartCoroutine(Healing());


        }
    }
    // ---------------------------------------- //
    // Useful Functions

    //You can call this function whenever to change the animation
    void ChangeAnimationState(string newState){
        if (currentAnimation == newState) return;
        animator.Play(newState);
        currentAnimation = newState;
    }
    float GetAngleTowardsMouse(){
        Vector3 mousePosition = playerControls.Land.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 positionComparedToPlayer = mousePosition - transform.position;
        return Mathf.Atan2(positionComparedToPlayer.y, positionComparedToPlayer.x) * Mathf.Rad2Deg;
    }
    bool DirectionalCollide(int direction, float distance, LayerMask target, float sideClip = 0.2f){
        Vector2 centerOfCollider = new Vector2(playerOffset.x, playerOffset.y);
        Vector2 sizeOfCollider = new Vector2(playerSize.x - sideClip * 2, playerSize.y - sideClip * 2);
        switch (direction) {
            case 1:
                // look up
                sizeOfCollider.y =  distance;
                centerOfCollider = new Vector2(centerOfCollider.x, centerOfCollider.y + (playerSize.y / 2) + (distance / 2));
                break;
            case 2:
                // look right
                sizeOfCollider.x = distance;
                centerOfCollider = new Vector2(centerOfCollider.x + (playerSize.y / 2) + (distance / 2), centerOfCollider.y);
                break;
            case 3:
                // look down
                sizeOfCollider.y = distance;
                centerOfCollider = new Vector2(centerOfCollider.x, centerOfCollider.y - (playerSize.y / 2) - (distance / 2));
                break;
            case 4:
                // look left
                sizeOfCollider.x = distance;
                centerOfCollider = new Vector2(centerOfCollider.x - (playerSize.y / 2) - (distance / 2), centerOfCollider.y);
                break;
        }
        return Physics2D.OverlapBox(centerOfCollider, sizeOfCollider, 0, target); 
    }
    // ---------------------------------------- //
    void Attack(InputAction.CallbackContext context)
    {


    }
    // Horizontal Movement
    void HorizontalMovement(){
        if (!disableMovement && !disableHorizontalMovement){
            if (!isCrouching) rb.velocity = new Vector2(speed * xMovement, rb.velocity.y);
            else if (isCrouching) rb.velocity = new Vector2(speed * xMovement * crouchSpeedPercent, rb.velocity.y);
        }
    }
    void Flip(){
        if (xMovement < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
            rotationPoint.transform.localScale = new Vector3(-1, 1, 1);
            // keeps rotationPoint child facing same way as before (double negative puts it back to 1)
        } else if (xMovement > 0) {
            transform.localScale = new Vector3(1, 1, 1);
            rotationPoint.transform.localScale = new Vector3(1, 1, 1); 
            // keeps rotationPoint child facing same way as before
        }
    }
    // ---------------------------------------- //
    // Jump Mechanics
    bool Grounded(){
        return DirectionalCollide(3, 0.1f, terrain, 0.25f);
    }
    void CoyateTimer(){
         if (!isGrounded && Grounded()){
            isGrounded = true;
            coyateTimeCounter = coyateTime;
        } else if (isGrounded && !Grounded()){
            coyateTimeCounter -= Time.fixedDeltaTime;
            if (coyateTimeCounter < 0) isGrounded = false;
        }
    }

    void TryToJump(InputAction.CallbackContext context){
        if (!disableMovement){
            if (context.phase == InputActionPhase.Started){
                jumpBufferCounter = jumpBuffer;
            } 
            if (context.phase == InputActionPhase.Canceled && rb.velocity.y > 0f){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * damper);
            } 
        }
    }
    void Jump(){
        if (!(disableMovement || DirectionalCollide(1, 0.2f, terrain))){
            if (isGrounded) doubleJump = true;
            if (jumpBufferCounter > 0 && (isGrounded || doubleJump || isWallSliding)){
                if (!isWallSliding){
                    if (!isGrounded) doubleJump = false; 
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                } else {
                    doubleJump = false;
                    StartCoroutine(WallJump());
                }
                isGrounded = false;
                jumpBufferCounter = 0;
            }
        }
    }
    IEnumerator WallJump(){
        disableMovement = true;
        rb.velocity = new Vector2(-xMovement * jumpForce * Mathf.Cos(wallJumpAngle * Mathf.Deg2Rad), jumpForce * Mathf.Sin(wallJumpAngle * Mathf.Deg2Rad));
        yield return new WaitForSeconds(wallJumpDuration);
        disableMovement = false;
    }
    // ---------------------------------------- //
    // Dash Functions
    void TryToDash(InputAction.CallbackContext context){
        if (canDash){
            StartCoroutine(JustDashed());
            StartCoroutine(Dashing());
        } 
    }
    IEnumerator Dashing(){
        rb.gravityScale = 0;
        disableMovement = true;
        tr.emitting = true;
        if (isCrouching && Grounded()) {
            rb.velocity = new Vector2(dashStrength *  transform.localScale.x, 0);
        } else {
            float x = (float)Mathf.Cos(Mathf.Deg2Rad * GetAngleTowardsMouse());
            float y = (float)Mathf.Sin(Mathf.Deg2Rad * GetAngleTowardsMouse());
            rb.velocity = new Vector2(x * dashStrength, y * dashStrength);
        }
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = gravity;
        disableMovement = false;
        tr.emitting = false;
    }
    IEnumerator JustDashed(){
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    // ---------------------------------------- //
    // Crouch
    void CrouchInput(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Started) wantsToCrouch = true;
        else if (context.phase == InputActionPhase.Canceled) wantsToCrouch = false;
    }
    void CheckIfCanUncrouch(){
        if (DirectionalCollide(1, (idleSize.y - crouchSize.y), terrain)) cantUncrouch = true;
        else cantUncrouch = false;
    }
    void Crouch(){
        if (wantsToCrouch != isCrouching){
            if (wantsToCrouch && !isWallSliding){
                ChangeAnimationState("Crouch");
                isCrouching = true;
                bc.size = crouchSize;
                playerSize = crouchSize;
                this.transform.Translate(new Vector3(0, -(idleSize.y - crouchSize.y) / 2, 0));
            } else if (!wantsToCrouch) {
                CheckIfCanUncrouch();
                if (!cantUncrouch) {
                    ChangeAnimationState("Idle");
                    this.transform.Translate(new Vector3(0, (idleSize.y - crouchSize.y) / 2, 0));
                    isCrouching = false;
                    bc.size = idleSize;
                    playerSize = idleSize;
                }
            }
        }
    }
    // ---------------------------------------- //
    // Wall Slide
    void WallSlide(){
        if (disableMovement) return;
        if (wallSlide) {
            rb.velocity = new Vector2 (rb.velocity.x, Mathf.Clamp(rb.velocity.y, -(wallSlideSpeed - 1.962f), float.MaxValue));
            // idk why but the slide speed was 1.962 more than what it should have been so subtracting fixes
            if (!isWallSliding){
                bc.size = wallSlideSize;
                isWallSliding = true;
                ChangeAnimationState("Wall Slide");
                disableHorizontalMovement = true;
            }
        } else if (isWallSliding){
            bc.size = idleSize;
            disableHorizontalMovement = false;
            isWallSliding = false;
            ChangeAnimationState("Idle");
        }
    }
    void IsWallSliding(){
        if (disableMovement) wallSlide = false;
        else wallSlide  = rb.velocity.y < 0 && !isCrouching && ((xMovement < 0 && DirectionalCollide(4, 0.1f, terrain)) || (xMovement > 0 && DirectionalCollide(2, 0.1f, terrain)));
    }
    // ---------------------------------------- //
     /*public void PauseCheck() {
        if(!(p.getHealth() == 0))
        {
            if (PausePanel.activeSelf == false && Input.GetButtonDown("Cancel")){
                PausePanel.SetActive(true);
                Time.timeScale = 0;
            } else if(PausePanel.activeSelf == true && Input.GetButtonDown("Cancel"))
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1;
            }

        }
    }*/
    public void DeathCheck() {
        if (p.getHealth() == 0 && DeathPanel.activeSelf == false)
        {
            DeathPanel.SetActive(true);
            Time.timeScale = 0;
        }



    }
    public void damage(int d)
    {
        p.decreaseHealth(d);
        //StartCoroutine(Shaking());
    }
    public IEnumerator Healing()
    {

        if (p.getHealth() < p.getMaxHealth() && !p.getRegen())
        {
            p.setRegenPause(true);
            if (p.getJustHit())
            {
                p.setJustHit(false);
                yield return new WaitForSeconds(3f);
            }
            yield return new WaitForSeconds(1f);
            if (!p.getJustHit())
            {
                p.increaseHealth(1);
            }
            p.setRegenPause(false);
        }
    }
    // ---------------------------------------- //
}