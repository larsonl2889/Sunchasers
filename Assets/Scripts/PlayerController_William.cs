using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController_Willliam : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isBuilding = false;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Controls playerControls;
    private SpriteRenderer SpriteRenderer;
    private Animator animator;
    private PlayerPlatformHandler playerPlatformHandler;
    Vector2 position;
    Stack<GameObject> objectsNear;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = rb.GetComponent<SpriteRenderer>();
        animator = rb.GetComponent<Animator>();
        playerControls = new Controls();
        playerPlatformHandler = GetComponent<PlayerPlatformHandler>();
        objectsNear = new Stack<GameObject>();
        
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
        
        playerControls.Player.Interact.performed += interact;
        playerControls.Player.Down.performed += GoDownPlatform;
        playerControls.Player.Click.performed += OnClick;
    }


    private void FixedUpdate()
    { 
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        animate();
        
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
    public void animate()
    {
        if (IsGrounded())
        {
            animator.SetBool("IsFalling", false);
            if (direction.x != 0)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
        else
        {
            animator.SetBool("IsFalling", true );
        }
        
    }
    public void onJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() == true)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
    }
   
    public void interact(InputAction.CallbackContext context)
    {
        if (objectsNear.Count > 0)
        {
            if (objectsNear.Peek().gameObject.CompareTag("buildWorkshop"))
            {
                isBuilding = true;
            }
            
            objectsNear.Peek().GetComponent<Interactable_William>().InvokeAction();
            
        }

    }
    public void OnClick(InputAction.CallbackContext context)
    {
        position = Mouse.current.position.ReadValue();
        if (isBuilding) {
            Debug.Log("BUILD!!");
        }

    }
    
    
    public bool IsGrounded()
    {
        return rb.velocity.y == 0;
    }

    public void GoDownPlatform(InputAction.CallbackContext context)
    {
        if(IsGrounded() == true)
        {
            playerPlatformHandler.GoDownPlatform();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("buildWorkshop"))
        {
            objectsNear.Push(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("buildWorkshop"))
        {
            objectsNear.Pop();
            isBuilding = false;
        }
    }
}
    

