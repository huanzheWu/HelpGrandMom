using UnityEngine;
using System.Collections;

public class ChessTouch : MonoBehaviour {
    /// <summary>
    /// 鼠标点击
    /// </summary>
    public void OnMouseDown()
    {
        SelectChess();
    }


    /// <summary>
    /// 确定选择的棋子
    /// </summary>
    internal void SelectChess()
    {
        if(ChessOperator.Instance.isBusy == false) //不繁忙的时候可以进行选择
        {
            //如果是第一次点击(即第一个棋子为空)，为第一个棋子赋值
            if (ChessOperator.Instance.chessItem1 == null)
            {
                ChessOperator.Instance.chessItem1 = this.gameObject.GetComponent<Chess>();

                ChessOperator.Instance.chessItem1.SeleteMe();  //发亮显示
            }
            //如果第二次点击(即第二个棋子为空)，为第二个棋子赋值
            else if (ChessOperator.Instance.chessItem2 == null)
            {
                ChessOperator.Instance.chessItem2 = this.gameObject.GetComponent<Chess>();
                ChessOperator.Instance.chessItem2.SeleteMe();  //发亮显示
                SwapTwoChess.Instance.SwapTwoChessObj(ChessOperator.Instance.chessItem1, ChessOperator.Instance.chessItem2);
            }//否则重置两个棋子
            else
            {
                ChessOperator.Instance.chessItem1.UnSeleteMe();
                ChessOperator.Instance.chessItem2.UnSeleteMe();
                ChessOperator.Instance.chessItem1 = null;
                ChessOperator.Instance.chessItem2 = null;
            }
        }
    }
      
}
