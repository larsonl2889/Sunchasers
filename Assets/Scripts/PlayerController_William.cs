using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Parts;
using Blocks;
using Cells;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public GameObject Slot;
    private Vector2 WorldPos;
    public GameObject currentBuildZone;
    
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
        playerControls.Player.RightClick.performed += OnRightClick;
        playerControls.Player.HotBar.performed += selectSlot;


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
            
            objectsNear.Peek().GetComponent<Interactable_William>().InvokeAction();
            
        }

    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (isBuilding) {
            if (currentBuildZone != null)
            {
                currentBuildZone.GetComponent<BuildingArea_Riley>().Build();
                currentBuildZone.GetComponentInParent<BuildAreaTest>().UpdateSteam();
            }
            
        }

    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        
        var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(pos: (Vector3)Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        Debug.Log("Hit");
        if(currentBuildZone != null)
        {
            if(rayHit.collider.gameObject.CompareTag("Part"))
            {
                Debug.Log("Method Ran");
                Part testPart = rayHit.collider.gameObject.GetComponentInParent<Part>();
                rayHit.collider.gameObject.GetComponentInParent<Part>().Extract();
                currentBuildZone.GetComponentInParent<BuildAreaTest>().UpdateSteam();
}
        }
        
    }
    public void selectSlot(InputAction.CallbackContext context)
    {
        int currSlotSelected = (int)context.ReadValue<float>();
        Debug.Log(currSlotSelected);
        
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
        
        if (other.gameObject.CompareTag("Interactable"))
        {
            
            other.gameObject.GetComponent<Interactable_William>().showPrompt(other.transform.position);
            objectsNear.Push(other.gameObject);
            
            
        }
        if (other.gameObject.CompareTag("buildWorkshop"))
        {
            currentBuildZone = other.gameObject;
            isBuilding = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            other.gameObject.GetComponent<Interactable_William>().hidePrompt();
            objectsNear.Pop();
            
        }
        if (other.gameObject.CompareTag("buildWorkshop"))
        {

            currentBuildZone = null;
            isBuilding = false;
        }
    }
   
}
    

