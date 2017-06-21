using UnityEngine;
using UnityEngine.UI;

public class SeasonController : MonoBehaviour
{
    private static SeasonController _instance;
    public static SeasonController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    [Header("背景")]
    public Image bgImg;
    [Header("春天背景")]
    public Sprite springSprite;
    [Header("夏天背景")]
    public Sprite summerSprite;
    [Header("秋天背景")]
    public Sprite autumnSprite;
    [Header("冬天背景")]
    public Sprite winterSprite;
    [Header("春天")]
    public GameObject spring;
    [Header("夏天")]
    public GameObject summer;
    [Header("秋天")]
    public GameObject autumn;
    [Header("冬天")]
    public GameObject winter;

    // 设置季节
    public void SetSeason(SeasonType seasonType)
    {
        switch (seasonType)
        {
            case SeasonType.Spring:
                // 春天上云
                bgImg.sprite = springSprite;
                spring.SetActive(true);
                summer.SetActive(false);
                autumn.SetActive(false);
                winter.SetActive(false);
                break;
            case SeasonType.Summer:
                // 夏天下雨
                bgImg.sprite = summerSprite;
                spring.SetActive(false);
                summer.SetActive(true);
                autumn.SetActive(false);
                winter.SetActive(false);
                break;
            case SeasonType.Autumn:
                // 秋天下云
                bgImg.sprite = autumnSprite;
                spring.SetActive(false);
                summer.SetActive(false);
                autumn.SetActive(true);
                winter.SetActive(false);
                break;
            case SeasonType.Winter:
                // 冬天下雪
                bgImg.sprite = winterSprite;
                spring.SetActive(false);
                summer.SetActive(false);
                autumn.SetActive(false);
                winter.SetActive(true);
                break;
            default:
                break;
        }
    }
}
