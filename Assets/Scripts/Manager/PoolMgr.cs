using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : UnitySingleton<PoolMgr>
{
    [Header("雨预制体")]
    public GameObject rainPrefab;
    [Header("风预制体")]
    public GameObject windPrefab;

    private Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();
    public override void Awake() { base.Awake(); Init(); }

    void Init()
    {
        // 雨对象池
        Pool rainPool = new Pool(Consts.Rain_Pool, Consts.Rain_Pool_Max, rainPrefab);
        poolDict.Add(Consts.Rain_Pool, rainPool);
        // 风对象池
        Pool windPool = new Pool(Consts.Wind_Pool, Consts.Wind_Pool_Max, windPrefab);
        poolDict.Add(Consts.Wind_Pool, windPool);
    }

    // 从对象池获取物体
    public GameObject Spawn(string poolName)
    {
        Pool pool;
        poolDict.TryGetValue(poolName, out pool);
        if (pool != null)
        {
            return pool.Spawn();
        }
        else
        {
            Debug.LogError(poolName + " Error");
            return null;
        }
    }

    // 将物体放回对象池
    public void Unspawn(GameObject go)
    {
        Pool pool;
        poolDict.TryGetValue(go.name, out pool);
        if (pool != null)
        {
            pool.Unspawn(go);
        }
        else
        {
            Debug.LogError(go.name + " Error");
        }
    }
}
