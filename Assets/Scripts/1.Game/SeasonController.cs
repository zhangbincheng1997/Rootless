using UnityEngine;
using UnityEngine.UI;

public class SeasonController : MonoBehaviour
{
    private static SeasonController _instance;
    public static SeasonController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    [Header("背景")]
    public Image bgImage;
    [Header("春天")]
    public Sprite springSprite;
    [Header("夏天")]
    public Sprite summerSprite;
    [Header("秋天")]
    public Sprite autumnSprite;
    [Header("冬天")]
    public Sprite winterSprite;
    [Header("雨精灵")]
    public Sprite[] rainSprites;
    [Header("雪精灵")]
    public Sprite[] snowSprites;
    [Header("遮罩")]
    public Sprite UIMask;

    // 季节类型
    private SeasonType seasonType;
    public void SetSeason(SeasonType seasonType) { this.seasonType = seasonType; }

    private int index = 0;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Consts.Switch_Time)
        {
            // 重置时间
            timer = 0;

            // 更换天气
            switch (seasonType)
            {
                case SeasonType.Spring:
                    // 春天正常
                    bgImage.sprite = springSprite;
                    this.GetComponent<Image>().sprite = UIMask;
                    break;
                case SeasonType.Summer:
                    // 夏天下雨
                    bgImage.sprite = summerSprite;
                    this.GetComponent<Image>().sprite = rainSprites[index++ % rainSprites.Length];
                    break;
                case SeasonType.Autumn:
                    // 秋天正常
                    bgImage.sprite = autumnSprite;
                    this.GetComponent<Image>().sprite = UIMask;
                    break;
                case SeasonType.Winter:
                    // 冬天下雪
                    bgImage.sprite = winterSprite;
                    this.GetComponent<Image>().sprite = snowSprites[index++ % snowSprites.Length];
                    break;
                default:
                    break;
            }
        }
    }
}
