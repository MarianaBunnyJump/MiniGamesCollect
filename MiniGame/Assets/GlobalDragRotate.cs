using UnityEngine;
using UnityEngine.EventSystems;

public class GlobeDragEvents : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float rotateSpeed = 1f;
    
    private Vector3 lastMousePosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = Input.mousePosition;
        Debug.Log("开始拖拽");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 delta = currentMousePosition - lastMousePosition;
        
        // 旋转逻辑（和之前一样）
        float rotationX = delta.y * rotateSpeed;   // 上下 -> 绕X轴
        float rotationY = -delta.x * rotateSpeed;  // 左右 -> 绕Y轴（负号让方向正确）
        
        transform.Rotate(rotationX, rotationY, 0, Space.World);
        
        lastMousePosition = currentMousePosition;
        
        Debug.Log($"拖拽中: delta({delta.x}, {delta.y})");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("结束拖拽");
    }
}