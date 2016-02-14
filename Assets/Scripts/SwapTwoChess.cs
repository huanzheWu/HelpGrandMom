//交换两个棋子

using UnityEngine;
using System.Collections;

public class SwapTwoChess : MonoBehaviour {

    public static SwapTwoChess Instance;    //静态实例
    void Awake ()
    {
        Instance = this;
    }
    /// <summary>
    /// 交换两个棋子
    /// </summary>
    /// <param name="chess1">第一个棋子</param>
    /// <param name="chess2">第二个棋子</param>
    internal void SwapTwoChessObj(Chess chess1, Chess chess2)
    {
        ChessOperator.Instance.isBusy = true;
        if(chess1 == null || chess2 == null)
        {
            Debug.LogError("SwapTwoChess.cs/SwapTwoChessObj参数确实");
        }
        //切换动画
        iTween.MoveTo(chess1.gameObject, chess2.gameObject.transform.position, 1f);
        iTween.MoveTo(chess2.gameObject, chess1.gameObject.transform.position, 1f);
        //核心交换算法
        SwapTwoChessItem(chess1, chess2);
    }

    /// <summary>
    /// 算法：交换两个棋子
    /// 更新交换后棋子所在列或所在行
    /// 更新集合中的棋子
    /// </summary>
    /// <param name="chess1">第一个棋子</param>
    /// <param name="chess2">第二个棋子</param>
    internal void SwapTwoChessItem(Chess chess1,Chess chess2)
    {
        Columns theColumnsChess1In=null;    //棋子一所在的列脚本
        Columns theColumnsChess2In=null;    //棋子二所在的列脚本

        int intChess1IndexInColumns = -999;  //棋子在该列的序号
        int intChess2IndexInColumns = -999;  //棋子在该列的序号

        theColumnsChess1In = chess1.fromColumns;  //取得棋子1当前所在列脚本
        theColumnsChess2In = chess2.fromColumns;  //取得棋子2当前所在列脚本

        if(theColumnsChess1In == null || theColumnsChess2In == null )
        {
            print("theColumnsChess1In" + theColumnsChess1In);
            print("theColumnsChess2In" + theColumnsChess2In);
            Debug.Log("SwapTwoChess.CS/SwapTwoChessItem参数不对");
            return;
        }

        //找到问题所在了！！
        //队列交换时没有

        //得到棋子1在列中的序号
        for (int i = 0; i < theColumnsChess1In.ChessList.Count; i++)
        {
            int ID1 = chess1.GetInstanceID();
            int ID2 = theColumnsChess1In.ChessList[i].GetInstanceID();
            if(chess1.GetInstanceID() == theColumnsChess1In.ChessList[i].GetInstanceID())
            {
                intChess1IndexInColumns = i;
            }
        }
        //得到棋子2在列中的序号
        for (int j = 0; j < theColumnsChess2In.ChessList.Count; j++)
        {
            int ID1 = chess2.GetInstanceID();
            int ID2 = theColumnsChess2In.ChessList[j].GetInstanceID();

            if(chess2.GetInstanceID() == theColumnsChess2In.ChessList[j].GetInstanceID())
            {
                intChess2IndexInColumns = j;
            }
        }

        //如果找不到，那就出错了
        if( -999==intChess1IndexInColumns || -999==intChess2IndexInColumns)
        {
            
            //重新调用关卡
            Application.LoadLevel(Application.loadedLevel);
            print("重新调用重新调用重新调用重新调用重新调用重新调用重新调用");
            print("intChess1IndexInColumns:" + intChess1IndexInColumns);
            print("intChess2IndexInColumns:" + intChess2IndexInColumns);
        }

        //更新棋子所属列"父子关系"
        chess1.transform.parent = theColumnsChess2In.transform;
        chess2.transform.parent = theColumnsChess1In.transform;

        //更新棋子所在列信息
        Columns fromTemp = chess1.fromColumns;
        chess1.fromColumns = chess2.fromColumns;
        chess2.fromColumns = fromTemp;

        //更新集合内容
        theColumnsChess1In.ChessList.RemoveAt(intChess1IndexInColumns);
        theColumnsChess1In.ChessList.Insert(intChess1IndexInColumns, chess2);

        theColumnsChess2In.ChessList.RemoveAt(intChess2IndexInColumns);
        theColumnsChess2In.ChessList.Insert(intChess2IndexInColumns, chess1);

        //print("+++++++++++++++++++++++++++++++++++++++++++++++++");
        //参数重置
        // 发暗显示
        ChessOperator.Instance.chessItem1.UnSeleteMe();
        ChessOperator.Instance.chessItem2.UnSeleteMe();

        ChessOperator.Instance.chessItem1 = null;
        ChessOperator.Instance.chessItem2 = null;
        //循环消除检查
        ChessOperator.Instance.StartCoroutine("CheckIfCanBurst");
    }
}


