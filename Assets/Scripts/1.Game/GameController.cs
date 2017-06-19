using UnityEngine;

public enum GameState
{
    Start, // 开始
    EventTime, // 事件
    GapTime, // 间隔
    GameWin, // 游戏胜利
    GameLose // 游戏失败
}

public enum SeasonType
{
    Spring, // 春
    Summer, // 夏
    Autumn, // 秋
    Winter // 冬
}

public enum EventType
{
    Worm, // 虫
    Rain, // 雨
    Wind // 风
}

public enum TreeType
{
    Sapling, // 树苗
    Small, // 小树
    Middle, // 中树
    Big // 大树
}

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    public GameState gameState { get; set; }
    private SeasonType seasonType;
    private EventType eventType;
    private TreeType treeType;

    // 当前日期
    private int nowDay = 0;
    // 滋润值
    private int rich = 0;
    // 健康值
    private int health = 0;

    // 计时器
    private float timer = 0;

    // 初始化
    void Start()
    {
        Time.timeScale = 1;
        gameState = GameState.Start;
        seasonType = SeasonType.Winter;
        eventType = EventType.Worm;
        treeType = TreeType.Sapling;
        nowDay = 0;
        rich = Consts.Start_Rich;
        health = Consts.Start_Health;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                // NEXT
                timer = 0;
                gameState = GameState.EventTime;
                // 设置季节
                seasonType = Util.GetNextSeason(seasonType, nowDay++);
                SeasonController.Instance.SetSeason(seasonType);
                // 设置日期
                UIController.Instance.OnDayChange(nowDay, seasonType);
                // 设置事件
                eventType = Util.GetEvent(seasonType);
                EventController.Instance.SetEvent(eventType);
                break;
            case GameState.EventTime:
                // 计时
                timer += Time.deltaTime;
                if (timer >= Consts.Event_Time)
                {
                    // NEXT
                    timer = 0;
                    gameState = GameState.GapTime;
                    // 取消事件
                    EventController.Instance.StopEvent(eventType);
                }
                break;
            case GameState.GapTime:
                // 计时
                timer += Time.deltaTime;
                if (timer >= Consts.Gap_Time)
                {
                    // 昼夜更替
                    DayController.Instance.NextDay();
                }
                break;
            case GameState.GameWin:
                // 游戏胜利
                Time.timeScale = 0;
                UIController.Instance.OnGameWin();
                break;
            case GameState.GameLose:
                // 游戏结束
                Time.timeScale = 0;
                UIController.Instance.OnGameLose();
                break;
            default:
                break;
        }
    }

    // 改变滋润值
    public void RichChange(int change)
    {
        rich = Mathf.Clamp(rich + change, 0, Consts.Max_Rich);
        UIController.Instance.OnRichChange(rich);

        if (rich == Consts.Max_Rich)
        {
            if (treeType == TreeType.Big)
            {
                // WIN
                gameState = GameState.GameWin;
            }
            else
            {
                // 生长
                GroundController.Instance.Grow(++treeType);
                // 重置
                UIController.Instance.OnRichChange(rich = 0);
            }
        }
    }

    // 改变健康值
    public void HealthChange(int change)
    {
        health = Mathf.Clamp(health + change, 0, Consts.Max_Health);
        UIController.Instance.OnHealthChange(health);

        if (health <= 0)
        {
            // LOSE
            gameState = GameState.GameLose;
        }
    }
}
