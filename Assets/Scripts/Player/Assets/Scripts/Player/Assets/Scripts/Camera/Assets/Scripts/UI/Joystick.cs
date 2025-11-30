using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;
    public float handleRange = 1f;
    
    private Vector2 input = Vector2.zero;
    private Canvas canvas;
    private Camera cam;
    
    public float Horizontal => input.x;
    public float Vertical => input.y;
    
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, eventData.position, cam, out position);
        
        position = position / (background.sizeDelta / 2);
        input = position.magnitude > 1f ? position.normalized : position;
        handle.anchoredPosition = input * (background.sizeDelta / 2) * handleRange;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
