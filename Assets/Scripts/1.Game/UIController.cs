using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    // TopBar
    public Button pauseBtn;
    public Button returnBtn;
    public Slider richBar;
    public Slider healthBar;
    public Text richText;
    public Text healthText;
    public Text dayText;
    public GameObject topBar;

    // GameOver
    public Button return2Btn;
    public Image winImg;
    public Image loseImg;
    public GameObject gameOver;

    private bool isShow = false;
    private bool isPause = false;

    void Start()
    {
        // 注册监听
        pauseBtn.onClick.AddListener(delegate { OnPauseBtnClick(); });
        returnBtn.onClick.AddListener(delegate { OnReturnBtnClick(); });
        return2Btn.onClick.AddListener(delegate { OnReturn2BtnClick(); });
    }

    void Update()
    {

#if UNITY_EDITOR
        // 点击
        if (Input.GetMouseButtonDown(1))
        {
            isShow = !isShow;
            pauseBtn.gameObject.SetActive(isShow); returnBtn.gameObject.SetActive(isShow);
        }
#elif UNITY_ANDROID
        // 滑动
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            isShow = !isShow;
            pauseBtn.gameObject.SetActive(isShow); returnBtn.gameObject.SetActive(isShow);
        }
#endif
    }

    //////////////////// TopBar

    // 暂停按钮
    void OnPauseBtnClick()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 1 : 0;
        // 隐藏按钮
        pauseBtn.gameObject.SetActive(false);
        returnBtn.gameObject.SetActive(false);
    }

    // 返回按钮
    void OnReturnBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        SceneMgr.Instance.LoadScene(Consts.Scene_Main);
        // 隐藏按钮
        pauseBtn.gameObject.SetActive(false);
        returnBtn.gameObject.SetActive(false);
    }

    // 滋润值
    public void OnRichChange(int v)
    {
        richBar.value = (float)v / Consts.Max_Rich;
        richText.text = v + " / " + Consts.Max_Rich;
    }

    // 健康值
    public void OnHealthChange(int v)
    {
        healthBar.value = (float)v / Consts.Max_Health;
        healthText.text = v + " / " + Consts.Max_Health;
    }

    // 下一天
    public void OnDayChange(int day, SeasonType seasonType)
    {
        dayText.text = "第 " + day + " 天" + " ❤ " + Util.GetSeasonString(seasonType);
    }

    //////////////////// GameOver

    // 返回按钮
    void OnReturn2BtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        SceneMgr.Instance.LoadScene(Consts.Scene_Main);
        // 隐藏TopBar
        topBar.SetActive(false);
    }

    // 游戏胜利
    public void OnGameWin()
    {
        // 隐藏TopBar
        topBar.SetActive(false);
        // 显示GameWin
        gameOver.SetActive(true);
        winImg.enabled = true;
        loseImg.enabled = false;
    }

    // 游戏失败
    public void OnGameLose()
    {
        // 隐藏TopBar
        topBar.SetActive(false);
        // 显示GameOver
        gameOver.SetActive(true);
        winImg.enabled = false;
        loseImg.enabled = true;
    }
}
