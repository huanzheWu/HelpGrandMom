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

    internal Chess chessItem1 = null;       //点击屏幕选择的第一个棋子
    internal Chess chessItem2 = null;       //点击屏幕选择的第二个棋子

    internal bool isBusy = false;   //系统是否繁忙(限制用户操作)

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
        isBusy = true;

        yield return new WaitForSeconds(0.2f);
        //为整个棋盘上的棋子分配邻居
        AssignNeighbor();
        //测试分配结果是否正确
        //Test();
        // 更新棋子标志位
        SetEveryChessFlag();
        //检测当前棋盘是否存在“消除”选项
        ifExitBurstItems  =  CheckExitBurstItem();

        if (ifExitBurstItems)
        {
           
            //删除当前棋盘的棋子
            DestryChessIfCanBurst();
            yield return new WaitForSeconds(0.5f);
            //增加新的棋子
            AddNewChessByTop();
            yield return new WaitForSeconds(0.5f);
            // 新的棋子下落动画处理
            PlayNewChessDropDown();
            yield return new WaitForSeconds(0.5f);
            //迭代循环检测
            StartCoroutine("CheckIfCanBurst"); //相当于递归
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            isBusy = false;
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
            for (int row = ColumnsManager.Instance.ColumnsArray[col].ChessList.Count-1; row >=0; row--) //倒过来写！！！
            {
                if (ColumnsManager.Instance.ColumnsArray[col].ChessList[row].canBurstCurrentChess == true) //如果当前的棋子可以删除
                {
                    //销毁游戏对象
                    ColumnsManager.Instance.ColumnsArray[col].ChessList[row].DestroyChess();
                    //同时销毁list中的脚本
                    ColumnsManager.Instance.ColumnsArray[col].ChessList.RemoveAt(row);
                    //该列需要增加的棋子数目增加1
                    ColumnsManager.Instance.ColumnsArray[col].intNeedAddingChessNumber += 1;
                }

            }
        }
    }

    /// <summary>
    /// 在列的顶部增加新的棋子
    /// </summary>
    private void AddNewChessByTop()
    {
        for (int col = 0; col < ColumnsManager.Instance.ColumnsArray.Length; col++)
        {
            ColumnsManager.Instance.ColumnsArray[col].AddNewChessByCurrentColumns();
        }
    }
 
    /// <summary>
    /// 整个棋盘每一列播放棋子的下落动画
    /// </summary>
    private void PlayNewChessDropDown()
    {
        for (int i = 0; i < ColumnsManager.Instance.ColumnsArray.Length; i++)
        {
            ColumnsManager.Instance.ColumnsArray[i].PlayNewChessDropDown();//每一列播放动画
        }
    }

}


