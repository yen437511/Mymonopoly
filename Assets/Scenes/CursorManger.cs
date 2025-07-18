using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [Header("一般游標貼圖")]
    public Texture2D defaultCursor;
    [Header("手型游標貼圖")]
    public Texture2D handCursor;
    [Header("熱點 (指尖位置)")]
    public Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
        // 單例
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // 一開始就把游標設成你想要的「一般游標」
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }

    /// <summary>切到手型游標</summary>
    public void UseHandCursor()
        => Cursor.SetCursor(handCursor, hotSpot, CursorMode.Auto);

    /// <summary>切回一般自訂游標</summary>
    public void UseDefaultCursor()
        => Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
}
