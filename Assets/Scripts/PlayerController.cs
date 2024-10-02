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
    private PlayerInput playerInput;
    

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();

        
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() == true)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            
        }
    }
    
    
    public bool IsGrounded()
    {
        return rb.velocity.y == 0;
    }
}
    

