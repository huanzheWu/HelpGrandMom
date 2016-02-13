/*
 *  脚本名称： ColumnsManager.cs
 *  作用： 管理所有的列
 *  日期：2016/2/13
 *  作者：huanzheWu
 *
*/
  
using UnityEngine;
using System.Collections;

public class ColumnsManager : MonoBehaviour {
    public static ColumnsManager Instance; //本类实例
    internal Columns[] ColumnsArray = new Columns[8]; //棋子列数数组

    void Awake()
    {
        Instance = this;
    }
   
    void Start()
    {
        for(int i =0;i<ColumnsArray.Length;i++)
        {
            //创建一个列对象，并加入集合中
            GameObject obj = new GameObject();
            ColumnsArray[i] = obj.AddComponent<Columns>();
            //确定父子关系
            obj.transform.parent = this.transform;
            //该对列的名称，后缀为列数
            obj.name = "columns_" + i;
            //确定当前的列编号
            Columns.Instance.intCurrentColumnsNumber = i;
        }
    }
    /// <summary>
    /// 为棋盘上每一列的棋子分配邻居
    /// </summary>
    internal void AssignNeighbor()
    {
        for(int i =0;i<ColumnsArray.Length;i++)
        {
            //每一列分别调用邻居分配函数进行列上棋子的邻居分配
            ColumnsArray[i].AssignNeighbor(); 
        }
    }



	

}
