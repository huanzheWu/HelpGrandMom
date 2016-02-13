/*
 *  脚本名称： ChessOperator.cs
 *  作用：1. 分配相邻棋子
 *        2. 检测棋盘面板的状态
 *  日期：2016/2/13
 *  版本: v1.0
 *  作者：huanzheWu
 *
*/
using UnityEngine;
using System.Collections;

public class ChessOperator : MonoBehaviour {
    public static ChessOperator Instance;//静态实例
    internal bool ifExitBurstItems=false; //当前棋盘是否存在消除项
    public void Awake ()
    {
        Instance = this;
    }
    public void Start()
    {
        StartCoroutine("CheckIfCanBurst"); //启动协程
    }

    IEnumerator CheckIfCanBurst()
    {
        yield return new WaitForSeconds(0.5f);
        //为整个棋盘上的棋子分配邻居
        AssignNeighbor();

        yield return new WaitForSeconds(0.5f);
        // 更新棋子标志位
        SetEveryChessFlag();
        
        yield return new WaitForSeconds(0.5f);

        //检测当前棋盘是否存在“消除”选项
        CheckExitBurstItem();
        yield return new WaitForSeconds(0.5f);

        if(ifExitBurstItems)
        {
            DestryChessIfCanBurst();
            //删除当前棋盘的棋子

            //增加新的棋子

            // 新的棋子下落动画处理

            //迭代循环检测
           // StartCoroutine("CheckIfCanBurst"); //相当于递归
        }
        else
        {
            print("当前棋盘没有消除选项");
        }
    }

    //棋盘分配邻居
    internal void AssignNeighbor()
    {
        ColumnsManager.Instance.AssignNeighbor();
        AssignStrForEveryChess();
    }

    //为每一个棋子分配字符串信息
    private void AssignStrForEveryChess()
    {
        for (int col = 0; col < ColumnsManager.Instance.ColumnsArray.Length; col++)
        {
            for (int row = 0; row < ColumnsManager.Instance.ColumnsArray[col].ChessList.Count; row++)
            {
                ColumnsManager.Instance.ColumnsArray[col].ChessList[row].AssignChessNeighborStr();
            }
        }
    }

    /// <summary>
    /// 循环一遍棋盘上所有的棋子，标志每颗棋子是否可以消除 
    /// </summary>
    private void SetEveryChessFlag()
    {
        for (int col = 0; col < ColumnsManager.Instance.ColumnsArray.Length; col++)
        {
            for (int row = 0; row < ColumnsManager.Instance.ColumnsArray[col].ChessList.Count; row++)
            {
                ColumnsManager.Instance.ColumnsArray[col].ChessList[row].MakeFlagIfCanBurest();
            }
        }
    }

    /// <summary>
    /// 检测棋盘上是否存在可以消除的选项
    /// 在标记之后执行该函数
    /// </summary>
    /// <returns>
    /// true:当前棋盘上存在可以消除的棋子
    /// false：当前棋盘上不存在可以消除的棋子
    /// </returns>
    private bool CheckExitBurstItem()
    {
        for (int col = 0; col < ColumnsManager.Instance.ColumnsArray.Length; col++)
        {
            for (int row = 0; row < ColumnsManager.Instance.ColumnsArray[col].ChessList.Count; row++)
            {
                if(ColumnsManager.Instance.ColumnsArray[col].ChessList[row].canBurstCurrentChess)
                {
                    ifExitBurstItems = true; //后续会改动，这里先这样
                    return true;
                }
            }
        }
        return false;
    }
    private void Test()
    {
        for (int col = 0; col < ColumnsManager.Instance.ColumnsArray.Length; col++)
        {
            for (int row = 0; row < ColumnsManager.Instance.ColumnsArray[col].ChessList.Count; row++)
            {
                ColumnsManager.Instance.ColumnsArray[col].ChessList[row].TestNeiborStr();
            }
        }
    }
    /// <summary>
    /// 删除掉可以删除的棋子
    /// </summary>
    private void DestryChessIfCanBurst()
    {
        for (int col = 0; col < ColumnsManager.Instance.ColumnsArray.Length; col++)
        {
            for (int row = 0; row < ColumnsManager.Instance.ColumnsArray[col].ChessList.Count; row++)
            {
                ColumnsManager.Instance.ColumnsArray[col].ChessList[row].DestroyChess();
            }
        }
    }
}


