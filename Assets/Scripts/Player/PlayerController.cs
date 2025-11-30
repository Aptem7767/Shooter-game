using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 7f;
    public float gravity = -20f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting;
    
    // Ввод с джойстика
    private Vector2 moveInput;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            controller = gameObject.AddComponent<CharacterController>();
            
        // Создаём точку проверки земли
        if (groundCheck == null)
        {
            GameObject gc = new GameObject("GroundCheck");
            gc.transform.SetParent(transform);
            gc.transform.localPosition = new Vector3(0, -1f, 0);
            groundCheck = gc.transform;
        }
    }
    
    void Update()
    {
        CheckGround();
        HandleMovement();
        ApplyGravity();
    }
    
    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }
    
    void HandleMovement()
    {
        // Получаем ввод
        float h = moveInput.x;
        float v = moveInput.y;
        
        // Для тестирования на ПК
        #if UNITY_EDITOR
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        #endif
        
        Vector3 move = transform.right * h + transform.forward * v;
        float speed = isSprinting ? sprintSpeed : moveSpeed;
        controller.Move(move * speed * Time.deltaTime);
    }
    
    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    // Вызывается из UI
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    
    public void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }
    
    public void SetSprint(bool sprint)
    {
        isSprinting = sprint;
    }
}
