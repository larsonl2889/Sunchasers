using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Parts;
using Blocks;
using Cells;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro.Examples;
using TMPro;

public class PlayerController_Willliam : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isBuilding = false;
    [SerializeField] public TextMeshProUGUI text;
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
    public int currSlotSelected = 1;
    public GameObject UI;
    public HotBarUI hotBarUI = null;
    

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
        playerControls.Player.Pause.performed += pause;


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
            if (currentBuildZone != null)//currentBuildZone.GetComponent<HotBar>().GetIndex()!=null
            {
                //Checks to see if the index at bar has a object before trying to place the bar at index.
                if (currentBuildZone.GetComponent<HotBar>().bar[currentBuildZone.GetComponent<HotBar>().index] != null) {
                    currentBuildZone.GetComponent<HotBar>().setBar();
                    currentBuildZone.GetComponent<BuildingArea_Riley>().build();
                }
                
                //hotBarUI.BuildUISlot();
    

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
                currentBuildZone.GetComponent<BuildingArea_Riley>().delete(rayHit.collider.gameObject);
                hotBarUI.RemoveUISlot();
            }
        }
        
    }
    public void selectSlot(InputAction.CallbackContext context)
    {
       // text.color = Color.yellow;
        if (currentBuildZone != null)
        {
            currSlotSelected = (int)context.ReadValue<float>();
            hotBarUI.HotBarNumSlots[currentBuildZone.GetComponent<HotBar>().index].GetComponent<TextMeshProUGUI>().color = Color.white;
            currentBuildZone.GetComponent<HotBar>().SetIndex(currSlotSelected - 1);
            hotBarUI.HotBarNumSlots[currSlotSelected-1].GetComponent<TextMeshProUGUI>().color = Color.yellow;
                //.gameObject.GetComponent<TMP_Text>().VertexColor=VertexColorCycler(vector3(0.96f, 0.66f, 0.22f));
            Debug.Log(currSlotSelected);
        }
        
        
    }
    public void pause(InputAction.CallbackContext context)
    {
        UI.GetComponent<PauseMenu>().changeMenuState();
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
            hotBarUI.hotbar = currentBuildZone.GetComponent<HotBar>();
            hotBarUI.updateImages();
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
            hotBarUI.hotbar = null;
            hotBarUI.updateImages();
            isBuilding = false;
        }
    }
   
}
    

