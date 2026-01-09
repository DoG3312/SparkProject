using UnityEngine;

public enum CursorType
{
    Default,
    Attack,
    Interact
}

public class CursorManager : MonoBehaviour
{
    [Header("Курсоры")]
    public Texture2D baseCursor;
    public Texture2D attackCursor;

    public Vector2 hotspot = Vector2.zero;
    public static CursorManager Instance { get; private set; }

    private CursorType currentMode;

    private void Start()
    {
        Instance = this;
    }

    public void SetCursor(CursorType type)
    {
        if (currentMode == type) return;

        currentMode = type;

        switch (type)
        {
            case CursorType.Default:
                Cursor.SetCursor(baseCursor, hotspot, CursorMode.Auto);
                break;
            case CursorType.Attack:
                Cursor.SetCursor(attackCursor, hotspot, CursorMode.Auto);
                break;
        }
    }
}
