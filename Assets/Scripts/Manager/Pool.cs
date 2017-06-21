using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private string poolName;
    private int maxCount;
    private GameObject poolGo;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="poolName"></param> 对象池名字
    /// <param name="maxCount"></param> 对象池最大数量
    /// <param name="poolGo"></param> 对象池物体
    public Pool(string poolName, int maxCount, GameObject poolGo)
    {
        this.poolName = poolName;
        this.maxCount = maxCount;
        this.poolGo = poolGo;
    }

    /// <summary>
    /// 对象池物体列表
    /// </summary>
    private List<GameObject> goList = new List<GameObject>();

    /// <summary>
    /// 从对象池获取物体
    /// </summary>
    /// <returns></returns>
    public GameObject Spawn()
    {
        foreach (GameObject go in goList)
        {
            // 物体非空并且隐藏状态
            if (go != null && !go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            }
        }

        // 超过对象池最大数量
        if (goList.Count > maxCount)
        {
            GameObject temp = goList[0];
            // 从池中移除
            goList.RemoveAt(0);
            // 销毁物体
            GameObject.Destroy(temp);
        }

        // 实例化物体
        GameObject goTemp = GameObject.Instantiate(poolGo) as GameObject;
        // 对象池同名
        goTemp.name = poolName;
        // 放到对象池
        goList.Add(goTemp);
        return goTemp;
    }

    /// <summary>
    /// 将物体放回对象池
    /// </summary>
    public void Unspawn(GameObject go)
    {
        go.SetActive(false);
    }
}
