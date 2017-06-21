using UnityEngine;

public class EventController : MonoBehaviour
{
    private static EventController _instance;
    public static EventController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    // 是否除虫
    private bool isWormOK = true;

    /// <summary>
    /// 编辑器模式
    /// </summary>
#if UNITY_EDITOR
    void Update()
    {
        if (eventType != EventType.Worm) { return; }

        // 键盘输入
        if (Input.GetKeyDown(KeyCode.F5))
        {
            isWormOK = true;
            MessageController.Instance.ShowMessage(Consts.Message_Good);
            StopEvent(EventType.Worm);
        }
    }
    /// <summary>
    /// 安卓模式
    /// </summary>
#elif UNITY_ANDROID
    // 旧的Y值
    private float oldY = 0;
    // 摇动次数
    private int shakeNum = 0;
    void Update()
    {
        if (eventType != EventType.Worm) { return; }

        // 重力感应
        float nowY = Input.acceleration.y;
        float dY = nowY - oldY;
        // 分量足够
        if (dY > Consts.Shake_Offset) { ++shakeNum; }
        // 次数足够
        if (shakeNum >= Consts.Shake_Num)
        {
            shakeNum = 0;
            isWormOK = true;
            MessageController.Instance.ShowMessage(Consts.Message_Good);
            StopEvent(EventType.Worm);
        }
    }
#endif

    private EventType eventType;
    public void SetEvent(EventType eventType)
    {
        this.eventType = eventType;
        switch (eventType)
        {
            case EventType.Worm:
                MessageController.Instance.ShowMessage(Consts.Message_Worm);
                GroundController.Instance.Show();
                isWormOK = false;
                break;
            case EventType.Rain:
                MessageController.Instance.ShowMessage(Consts.Message_Rain);
                InvokeRepeating("Raining", 0, Consts.Rain_Time);
                break;
            case EventType.Wind:
                MessageController.Instance.ShowMessage(Consts.Message_Wind);
                InvokeRepeating("Winding", 0, Consts.Wind_Time);
                break;
            default:
                break;
        }
    }

    public void StopEvent(EventType eventType)
    {
        switch (eventType)
        {
            case EventType.Worm:
                // 停止出虫
                GroundController.Instance.Hide();
                // 增加两点健康
                if (isWormOK) { GameController.Instance.HealthChange(Consts.Worm_Reward); }
                // 扣除五点健康
                else { GameController.Instance.HealthChange(Consts.Worm_Punish); }
                break;
            case EventType.Rain:
                // 停止下雨
                CancelInvoke("Raining");
                break;
            case EventType.Wind:
                // 停止刮风
                CancelInvoke("Winding");
                break;
            default:
                break;
        }
    }

    void Raining()
    {
        // 从对象池获取物体
        GameObject go = PoolMgr.Instance.Spawn(Consts.Rain_Pool);
        go.transform.SetParent(this.transform);
        go.transform.localPosition = new Vector3(Random.Range(-Consts.Weather_Offset, Consts.Weather_Offset), 0, 0);
        go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-Consts.Weather_Angle, Consts.Weather_Angle));
    }

    void Winding()
    {
        // 从对象池获取物体
        GameObject go = PoolMgr.Instance.Spawn(Consts.Wind_Pool);
        go.transform.SetParent(this.transform);
        go.transform.localPosition = new Vector3(Random.Range(-Consts.Weather_Offset, Consts.Weather_Offset), 0, 0);
        go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-Consts.Weather_Angle, Consts.Weather_Angle));
    }
}