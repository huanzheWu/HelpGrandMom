/*
 *  脚本名称： Columns.cs
 *  作用：1. 生成一列棋子,进而可生成整个棋盘
 *        2. 本脚本由ColumnsManager脚本动态生成
 *  日期：2016/2/13
 *  版本: v1.0
 *  作者：huanzheWu
 *
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Columns : MonoBehaviour
{
    public static Columns Instance;    //本类实例
    internal List<Chess> ChessList = new List<Chess>();//棋子集合
    internal int intCurrentColumnsNumber = 0;//当前列编号
    private float floChessScale;        //棋子的大小
    private float floChessColumnsSpace;//棋子列间间距
    internal int intRowNumberByCurrentColums;//当前行的列数量

    internal int remainNumByColAfterDes; //当前列棋子消除后的剩余数量

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //从GameManager脚本中得到每一列棋子的数量
        intRowNumberByCurrentColums = GameManager.Instance.InRowNumber;
        //从GameManager脚本中得到每一个棋子的大小
        floChessScale = GameManager.Instance.floChessScale;
        //从GameManager脚本中得到每一列棋子的间距
        floChessColumnsSpace = GameManager.Instance.floColumnSpace;
        //初始化字段
        remainNumByColAfterDes = intRowNumberByCurrentColums;

        //生成一列棋子
        for(int row = 0;row < intRowNumberByCurrentColums ; row++)
        {
            //我们有六张图，随机生成0~5这六个数字,取得随机的一个棋子预设
            int intRandomNumber = Random.Range(0, 6);
            GameObject chessPrefabs = GameManager.Instance.ChessPrefabsArray[intRandomNumber];

            //依据取得的预设，克隆出一个棋子，并放在棋盘正确位置
            GameObject chessObj = Instantiate(chessPrefabs,
                new Vector3(intCurrentColumnsNumber * floChessColumnsSpace, -row, chessPrefabs.transform.position.z),
                Quaternion.identity) as GameObject;

            //存入集合中
            ChessList.Add(chessObj.GetComponent<Chess>());
            //确定父子关系
            chessObj.transform.parent = this.transform;
            //确定棋子的大小
            chessObj.transform.localScale = new Vector3(floChessScale, floChessScale, floChessScale);
            //标注棋子所属的列
            //chessObj.GetComponent<Chess>().fromColumns = this;
        }

    }


    /// <summary>
    /// 该函数为一列棋子中每个棋子的邻居数组赋值
    /// 即为该列棋子的数组ChessList赋值
    /// </summary>
    internal void AssignNeighbor()
    {
        for(int i=0;i<ChessList.Count;i++)
        {
            if (ChessList[i] == null)
                continue;
            //第一列的棋子，位于棋盘的最左边，没有左邻居
            if(intCurrentColumnsNumber == 0)
            {
                //把左邻居置空
                ChessList[i].neighborChessArray[0] = null;
            }
            else
            {
                //如果不是第一列，则左邻居等于左相邻列同一行的棋子
                ChessList[i].neighborChessArray[0]
                    = ColumnsManager.Instance.ColumnsArray[intCurrentColumnsNumber - 1].ChessList[i];
            }
            //同理得，最后一列没有右邻居
            if(intCurrentColumnsNumber== ColumnsManager.Instance.ColumnsArray.Length-1)
            {
                //把右邻居置空
                ChessList[i].neighborChessArray[1] = null;
            }
            else
            {
                //如果不是最后一列，则右邻居等于右相邻列同一行的棋子
                ChessList[i].neighborChessArray[1]
                    = ColumnsManager.Instance.ColumnsArray[intCurrentColumnsNumber + 1].ChessList[i];
            }
            //如果是第一行棋子，它没有上邻居
            if(i==0)
            {
                ChessList[0].neighborChessArray[2] = null;
            }
            else
            {
                ChessList[0].neighborChessArray[2] =
                    ColumnsManager.Instance.ColumnsArray[intCurrentColumnsNumber].ChessList[i - 1];
            }
            //同理，最下一行棋子没有下邻居
            if(i==intRowNumberByCurrentColums-1)
            {
                ChessList[i].neighborChessArray[3] = null;
            }
            else
            {
                ChessList[i].neighborChessArray[3]
                    = ColumnsManager.Instance.ColumnsArray[intCurrentColumnsNumber].ChessList[i + 1];
            }

        }
    }


}

