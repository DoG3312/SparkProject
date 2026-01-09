using UnityEngine;

public static class MousePosition
{
    public static LayerMask targetLayer = LayerMask.GetMask("Surface");
    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, targetLayer))
        {
            return hitInfo.point;
        }
        Debug.Log("Тут что-то не так");
        return Vector3.zero;
    }
}
