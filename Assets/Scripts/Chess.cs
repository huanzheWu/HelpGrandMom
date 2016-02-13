/*
 *  脚本名称： Chess.cs
 *  作用： 管理每一颗棋子，为棋子分配邻居字符串
 *  日期：2016/2/13
 *  作者：huanzheWu
 *
*/

using UnityEngine;
using System.Collections;

public class Chess : MonoBehaviour {
    public static Chess Instance;
    internal Columns fromColumns;      //该棋子属于哪一列
    //每个棋子的邻居，数组索引0/1/2/3代表上/下/左/右邻居
    internal Chess[] neighborChessArray = new Chess[4];
    //是否能够消除当前棋子
    internal bool canBurstCurrentChess = false; 

    //邻居字符串，初始化时指定为“None”
    private string strChessLest1 = "None";
    private string strChessLest2 = "None";
    private string strChessRight1 = "None";
    private string strChessRight2 = "None";
    private string strChessUp1 = "None";
    private string strChessUp2 = "None";
    private string strChessDown1 = "None";
    private string strChessDown2 = "None";
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 检测当前棋子是否可以消除
    /// </summary>
    /// <param name="chessobj">指定的棋子</param>
    /// <returns>
    /// true: 棋子可以消除
    /// false : 棋子不满足消除的条件
    /// </returns>
    internal bool CanBurstByChess(Chess chessObj)
    {
        bool canBurst = false; //默认不可以消除

        //判断是否满足消除的条件
        if (
            (chessObj.gameObject.name == strChessLest1 && chessObj.gameObject.name == strChessLest2) ||
            (chessObj.gameObject.name == strChessLest1 && chessObj.gameObject.name == strChessRight1) ||
            (chessObj.gameObject.name == strChessRight1 && chessObj.gameObject.name == strChessRight2) ||
            (chessObj.gameObject.name == strChessUp1 && chessObj.gameObject.name == strChessUp2) ||
            (chessObj.gameObject.name == strChessUp1 && chessObj.gameObject.name == strChessDown1) ||
            (chessObj.gameObject.name == strChessDown1 && chessObj.gameObject.name == strChessDown2)
           )
        {
            canBurst = true;
        }
        else
            canBurst = false;
        return canBurst;
    }
    /// <summary>
    /// 当前的棋子是否可以消除
    /// </summary>
    /// <returns>
    /// true:当前棋子可以消除
    /// false:当前棋子不可以消除
    /// </returns>
    internal void  MakeFlagIfCanBurest()
    {
        if (CanBurstByChess(this))
        {
            canBurstCurrentChess = true;
        }
        else
        {
            canBurstCurrentChess=false ;
        }

    }

    /// <summary>
    /// 测试当前棋子的邻居棋子的字符串信息
    /// </summary>
    public void  TestNeiborStr()
    {
        print("");
        print("");
        print("strChessLest1:" + strChessLest1);
        print("strChessLest2:" + strChessLest2);
        print("strChessRight1:" + strChessRight1);
        print("strChessRight2:" + strChessRight2);
        print("strChessUp1:" + strChessUp1);
        print("strChessUp2:" + strChessUp2);
        print("strChessDown1:" + strChessDown1);
        print("strChessDown2:" + strChessDown2);
    }


    //为每个棋子的邻居字符串赋值
    internal void AssignChessNeighborStr()
    {
        if(null !=  neighborChessArray[0]) //如果有左1邻居
        {
            strChessLest1 = neighborChessArray[0].name; //为左1邻居赋值
            if(null != neighborChessArray[0].neighborChessArray[0])//如果有左2邻居
            {
                strChessLest2 = neighborChessArray[0].neighborChessArray[0].name; //为左2邻居赋值
            }
        }

        if (null != neighborChessArray[1]) //如果有右1邻居
        {
            strChessRight1 = neighborChessArray[1].name; //为右1邻居赋值
            if (null != neighborChessArray[1].neighborChessArray[1])//如果有右2邻居
            {
                strChessRight2 = neighborChessArray[1].neighborChessArray[1].name; //为右2邻居赋值
            }
        }

        if (null != neighborChessArray[2]) //如果有上1邻居
        {
            strChessUp1 = neighborChessArray[2].name; //为上1邻居赋值
            if (null != neighborChessArray[2].neighborChessArray[2])//如果有上2邻居
            {
                strChessUp2 = neighborChessArray[2].neighborChessArray[2].name; //为上2邻居赋值
            }
        }

        if (null != neighborChessArray[3]) //如果有下1邻居
        {
            strChessDown1 = neighborChessArray[3].name; //为下1邻居赋值
            if (null != neighborChessArray[3].neighborChessArray[3])//如果有下2邻居
            {
                strChessDown2 = neighborChessArray[3].neighborChessArray[3].name; //为下2邻居赋值
            }
        }

    }
    
    /// <summary>
    /// 删除棋子(只是删除了类所属对象，还需要删除列集合中的棋子)
    /// </summary>
    internal void DestroyChess()
    {
        if(canBurstCurrentChess)
        {
            Destroy(this.gameObject);
        }
    }

}
