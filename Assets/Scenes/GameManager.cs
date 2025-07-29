using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // public ShopUI shopUI;
    public List<Transform> boardPoints;    // 盤上 40 個格子的 Transform
    public Player player;           // 玩家清單
    public Text messageText;               // 文字
    public Text moneyText;               // 金額
    // private int currentPlayerIndex = 0;
    public bool isMoving = false; //人物是否移動中

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        messageText.gameObject.SetActive(false);
        StartCoroutine(MovePlayer(player, 1));
        // rollDiceButton.onClick.AddListener(OnRollDice);
        // UpdateTurnUI();
    }

    public void OnRollDice(int diceValue)
    {
        if (isMoving) return;
        StartCoroutine(MovePlayer(player, diceValue));
    }

    IEnumerator MovePlayer(Player p, int steps)
    {
        isMoving = true;
        float speed = 600f;    // 移動速度 (單位：格/秒)
        int remaining = steps;

        while (remaining > 0)
        {
            // 算出下一格索引
            int nextIndex = (p.positionIndex + 1) % boardPoints.Count;
            Vector3 targetPos = boardPoints[nextIndex].position;

            // 從當前位置，平滑移動到 targetPos
            while (Vector3.Distance(p.pieceTransform.position, targetPos) > 1f)
            {
                p.pieceTransform.position = Vector3.MoveTowards(
                    p.pieceTransform.position,
                    targetPos,
                    speed * Time.deltaTime
                );
                yield return null;
            }

            // 確保完全對齊
            p.pieceTransform.position = targetPos;
            p.positionIndex = nextIndex;
            remaining--;

            // 玩家經過起點
            if (nextIndex == 0)
            {
                // 1) 啟動所有刷新
                for (int i = 0; i < boardPoints.Count; i++)
                {
                    // 跳過第 8 格 (index 7) 和第 23 格 (index 22)
                    if (i == 0 || i == 7 || i == 22)
                        continue;
                    var tileTf = boardPoints[i];
                    var tile = tileTf.GetComponent<Tile>();
                    if (tile != null)
                        StartCoroutine(tile.RefreshContents());
                }
                // 2) 等所有都刷新完畢
                yield return new WaitUntil(() =>
                    boardPoints.All(t =>
                    {
                        var tile = t.GetComponent<Tile>();
                        return tile == null || tile.IsRefreshing == false;
                    })
                );
                // 小停頓，讓玩家看清刷新結果
                yield return new WaitForSeconds(0.5f);
            }

            // 玩家走到骰子點數位置
            if (remaining == 0)
            {
                var tile = boardPoints[nextIndex].GetComponent<Tile>();
                string msg = "";
                if (tile != null && tile.GetComponent<SpriteRenderer>().sprite != null)
                {
                    Sprite s = tile.GetComponent<SpriteRenderer>().sprite;
                    if (s == tile.spriteA)
                    {
                        msg = "-200";
                        UpdateMoney(-200);
                        //StartCoroutine(UpdateMoney(-200));
                    }
                    else if (s == tile.spriteB)
                    {
                        msg = "+200";
                        UpdateMoney(200);
                        //StartCoroutine(UpdateMoney(200));
                    }
                    else if (s == tile.spriteC)
                    {
                        msg = "你走到了 C！";
                    }
                    else if (s == tile.spriteD)
                    {
                        ShopUI.Instance.Show();
                    }
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    StartCoroutine(ShowMessage(msg, 3f));
                }
            }
            // 正常走格之間稍微停頓
            yield return new WaitForSeconds(0.1f);
        }
        // NextTurn();
        isMoving = false;
    }

    /// <summary>
    /// 在 UI 上顯示文字，等幾秒後自動隱藏
    /// </summary>
    IEnumerator ShowMessage(string msg, float duration)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        messageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 修改金額
    /// </summary>
    void UpdateMoney(int money)
    {
        int start = player.money;
        int end   = start + money;
        float duration = 1f;       // 動畫長度 1 秒
        if (money != 0)
        {
            DOTween.To(
            () => start,
            x => {
                start = x;
                moneyText.text = x.ToString();
            },
            end,
            duration
        )
        .SetEase(Ease.OutQuad)
        .OnComplete(() => player.money = end);
        }
    }
    
    //void NextTurn() {
    //currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    //UpdateTurnUI();
    //}

    // void UpdateTurnUI() {
    //     turnIndicator.text = $"輪到玩家 {currentPlayerIndex+1}";
    // }
}
