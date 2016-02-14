using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;   //游戏管理器静态实例
    public GameObject[] ChessPrefabsArray; //棋子预设数组
    public int InRowNumber = 5; //棋盘的行数量
    public float floColumnSpace = 1;//棋盘的列间距
    public float floChessScale = 1;//棋子大小的缩放

    void Awake()
    {
        Instance = this;
    }
}
