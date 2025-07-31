using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Dice : MonoBehaviour
{
    public static Dice Instance { get; private set; }

    [Header("骰子點數貼圖 (1~6)")]
    public Sprite[] diceSprites;         // 6 張貼圖，陣列長度要設 6
    public float rollDuration = 1f;      // 動畫總長
    public float frameInterval = 0.1f;  // 每幾秒換一次圖

    public bool isDisabled = false;
    private SpriteRenderer sr;
    public bool isRolling = false;      // 正在骰動旗標
    public GameManager gameManager;

    void Awake()
    {
        Instance = this;
        sr = GetComponent<SpriteRenderer>();
        if (diceSprites.Length != 6)
            Debug.LogError("請在 Inspector 填滿 6 張骰子貼圖！");
    }

    void OnMouseEnter()
    {
        if (!isDisabled)
            CursorManager.Instance.UseHandCursor();
    }

    void OnMouseExit()
    {
        //if (!isDisabled)
            CursorManager.Instance.UseDefaultCursor();
    }

    void OnMouseDown()
    {
        // 如果滑鼠正好在任何 UI 元件之上，就跳過
        // if (EventSystem.current.IsPointerOverGameObject())
        if (isDisabled)
            return;
        if (gameManager.isMoving || isRolling)
            return;
        StartCoroutine(Roll());
    }

    /// <summary>
    /// 擲骰，並切換對應貼圖
    /// </summary>
    private IEnumerator Roll()
    {
        isRolling = true;

        float elapsed = 0f;
        // 在 animation 期間快速亂切
        while (elapsed < rollDuration)
        {
            int rand = Random.Range(0, 6);
            sr.sprite = diceSprites[rand];
            yield return new WaitForSeconds(frameInterval);
            elapsed += frameInterval;
        }

        int result = Random.Range(0, 6);          // 1～6
        sr.sprite = diceSprites[result];      // 對應陣列索引 0～5

        // 把 result 傳給 GameManager 處理移動
        gameManager.OnRollDice(result + 1);
        isRolling = false;
    }
}
