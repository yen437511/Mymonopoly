using UnityEngine;

[System.Serializable]
public class Player {
    public Transform pieceTransform;    // 棋子物件
    public int positionIndex = 0;       // 目前格子編號
    public int money = 1500;            // 起始金額
}
