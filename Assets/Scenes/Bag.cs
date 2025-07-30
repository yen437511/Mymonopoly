using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Bag : MonoBehaviour
{
    public static Bag Instance { get; private set; }
    public bool isDisabled = false;

    void Awake()
    {
        Instance = this;
    }

    void OnMouseEnter()
    {
        if (!isDisabled)
            CursorManager.Instance.UseHandCursor();
    }

    void OnMouseExit()
    {
        if (!isDisabled)
            CursorManager.Instance.UseDefaultCursor();
    }
}
