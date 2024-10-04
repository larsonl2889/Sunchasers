using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Controls playerControls;
    private SpriteRenderer SpriteRenderer;
    

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = rb.GetComponent<SpriteRenderer>();
        playerControls = new Controls();
        
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Player.BuildMode.performed += toggleBuildMode;
        playerControls.Player.Interact.performed += interact;
    }


    private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        
    }
    
    public void onMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction.x > 0)
        {
            SpriteRenderer.flipX = true;
        }
        else if(direction.x < 0) {
        
            SpriteRenderer.flipX = false;
        }
    }
    public void onJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() == true)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            
        }
    }
    public void toggleBuildMode(InputAction.CallbackContext context)
    {
        Debug.Log("Build Mode");
 
    }
    public void interact(InputAction.CallbackContext context)
    {
        Debug.Log("interact");
    }

    
    
    public bool IsGrounded()
    {
        return rb.velocity.y == 0;
    }
}
    

