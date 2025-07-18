using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [Header("可供顯示的圖案 (依機率對應 A, B, C, D)")]
    public Sprite spriteA;   // 30%
    public Sprite spriteB;   // 20%
    public Sprite spriteC;   //  5%
    public Sprite spriteD;   //  5%

    [Header("動畫設定")]
    public float showDuration = 0.2f;   // 顯示動畫時長
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private SpriteRenderer sr;
    public bool IsRefreshing { get; private set; }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public IEnumerator RefreshContents()
    {
        IsRefreshing = true;

        // 1. 清空原本貼圖
        sr.sprite = null;
        transform.localScale = Vector3.zero;

        // 2. 隨機挑選
        float r = Random.value;  // 0.0 ~ 1.0
        Sprite toShow = null;

        if (r < 0.30f)              toShow = spriteA;
        else if (r < 0.30f + 0.20f) toShow = spriteB;
        else if (r < 0.30f + 0.20f + 0.05f) toShow = spriteC;
        else if (r < 0.30f + 0.20f + 0.05f + 0.05f) toShow = spriteD;
        // 剩下的機率 (0.40) toShow 保持為 null

        // 3. 若有選到圖，做顯示＋動畫
        if (toShow != null)
        {
            sr.sprite = toShow;

            float elapsed = 0f;
            while (elapsed < showDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / showDuration);
                float s = scaleCurve.Evaluate(t);
                transform.localScale = Vector3.one * s;
                yield return null;
            }
            transform.localScale = Vector3.one * 1.2f;
        }

        // 4. 完成
        yield return null;
        IsRefreshing = false;
    }
}
