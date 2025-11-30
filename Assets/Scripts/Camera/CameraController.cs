using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float sensitivity = 100f;
    public Vector3 offset = new Vector3(0, 2f, -4f);
    
    public float minVerticalAngle = -60f;
    public float maxVerticalAngle = 60f;
    
    private float rotationX = 0f;
    private float rotationY = 0f;
    private Vector2 lookInput;
    
    void LateUpdate()
    {
        if (target == null) return;
        
        HandleRotation();
        FollowTarget();
    }
    
    void HandleRotation()
    {
        float lookX = lookInput.x * sensitivity * Time.deltaTime;
        float lookY = lookInput.y * sensitivity * Time.deltaTime;
        
        #if UNITY_EDITOR
        lookX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        lookY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        #endif
        
        rotationY += lookX;
        rotationX -= lookY;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        
        if (target != null)
            target.rotation = Quaternion.Euler(0, rotationY, 0);
    }
    
    void FollowTarget()
    {
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * Mathf.Abs(offset.z)) + Vector3.up * offset.y;
        
        transform.position = position;
        transform.rotation = rotation;
    }
    
    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }
}
