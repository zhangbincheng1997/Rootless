using UnityEngine;

public class Util
{
    /// <summary>
    ///         出虫      降水          刮风
    /// 春天      50      30(80)      30(110)  
    /// 夏天      30      50(80)      30(110)  
    /// 秋天      30      30(60)      50(110)
    /// 冬天      30      50(80)      30(110)
    /// </summary>
    public static EventType GetEvent(SeasonType seasonType)
    {
        EventType eventType = EventType.Worm;
        int random = Random.Range(0, 110);
        switch (seasonType)
        {
            case SeasonType.Spring:
                if (random <= 50)
                {
                    eventType = EventType.Worm;
                }
                else if (random <= 80)
                {
                    eventType = EventType.Rain;
                }
                else
                {
                    eventType = EventType.Wind;
                }
                break;
            case SeasonType.Summer:
                if (random <= 30)
                {
                    eventType = EventType.Worm;
                }
                else if (random <= 80)
                {
                    eventType = EventType.Rain;
                }
                else
                {
                    eventType = EventType.Wind;
                }
                break;
            case SeasonType.Autumn:
                if (random <= 30)
                {
                    eventType = EventType.Worm;
                }
                else if (random <= 60)
                {
                    eventType = EventType.Rain;
                }
                else
                {
                    eventType = EventType.Wind;
                }
                break;
            case SeasonType.Winter:
                if (random <= 30)
                {
                    eventType = EventType.Worm;
                }
                else if (random <= 80)
                {
                    eventType = EventType.Rain;
                }
                else
                {
                    eventType = EventType.Wind;
                }
                break;
            default:
                break;
        }
        return eventType;
    }

    /// <summary>
    /// 每两天一季节
    /// 四季一次轮回
    /// </summary>
    public static SeasonType GetNextSeason(SeasonType seasonType, int nowDay)
    {
        if (nowDay % 2 == 1) { return seasonType; }
        if (seasonType == SeasonType.Spring) { return SeasonType.Summer; }
        if (seasonType == SeasonType.Summer) { return SeasonType.Autumn; }
        if (seasonType == SeasonType.Autumn) { return SeasonType.Winter; }
        if (seasonType == SeasonType.Winter) { return SeasonType.Spring; }
        return seasonType;
    }

    /// <summary>
    /// 返回季节对应中文
    /// </summary>
    public static string GetSeasonString(SeasonType seasonType)
    {
        if (seasonType == SeasonType.Spring) { return "春天"; }
        if (seasonType == SeasonType.Summer) { return "夏天"; }
        if (seasonType == SeasonType.Autumn) { return "秋天"; }
        if (seasonType == SeasonType.Winter) { return "冬天"; }
        return "未知";
    }
}
