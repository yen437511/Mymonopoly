using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ResponsiveGrid : MonoBehaviour
{
    public int columns = 3;

    void Start()
    {
        var grid = GetComponent<GridLayoutGroup>();
        // 取得父容器（Content）的寬度
       
        float totalWidth = (transform as RectTransform).rect.width;
        float spacing = grid.spacing.x * (columns - 1);
        float cellWidth = (totalWidth - spacing - grid.padding.left - grid.padding.right) / columns;
        // 設定 cellSize
        grid.cellSize = new Vector2(cellWidth, grid.cellSize.y);
    }
}
