using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    // Button
    public Button startBtn;
    public Button optionBtn;
    public Button ruleBtn;
    public Button quitBtn;

    // Option
    public GameObject optionPanel;
    public Slider musicSlider;
    public Slider effectSlider;
    public Button closeOptionBtn;

    // Rule
    public GameObject rulePanel;
    public Button closeRuleBtn;

    void Start()
    {
        startBtn.onClick.AddListener(delegate { OnStartBtnClick(); });
        optionBtn.onClick.AddListener(delegate { OnOptionBtnClick(); });
        ruleBtn.onClick.AddListener(delegate { OnRuleBtnClick(); });
        quitBtn.onClick.AddListener(delegate { OnQuitBtnClick(); });

        musicSlider.onValueChanged.AddListener(delegate { OnMusicSlider(musicSlider.value); });
        effectSlider.onValueChanged.AddListener(delegate { OnEffectSlider(effectSlider.value); });
        closeOptionBtn.onClick.AddListener(delegate { OnOptionCloseBtnClick(); });
        closeRuleBtn.onClick.AddListener(delegate { OnRuleCloseBtnClick(); });
    }

    // 开始按钮
    void OnStartBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        SceneMgr.Instance.LoadScene(Consts.Scene_Game);
        // 隐藏按钮
        startBtn.gameObject.SetActive(false);
        optionBtn.gameObject.SetActive(false);
        ruleBtn.gameObject.SetActive(false);
        quitBtn.gameObject.SetActive(false);
    }

    // 设置按钮
    void OnOptionBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        optionPanel.SetActive(true);
        // 设置滑块
        musicSlider.value = AudioMgr.Instance.GetMusicVolume();
        effectSlider.value = AudioMgr.Instance.GetEffectVolume();
    }

    // 提示按钮
    void OnRuleBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        rulePanel.SetActive(true);
    }

    // 退出按钮
    void OnQuitBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        Application.Quit();
    }

    ////////////////////////////////////////

    // 调整音乐大小
    void OnMusicSlider(float v)
    {
        AudioMgr.Instance.SetMusicVolume(v);
    }

    // 调整音效大小
    void OnEffectSlider(float v)
    {
        AudioMgr.Instance.SetEffectVolume(v);
    }

    // 关闭设置面板
    void OnOptionCloseBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        optionPanel.SetActive(false);
    }

    ////////////////////////////////////////

    // 关闭提示面板
    void OnRuleCloseBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Click_Effect);
        rulePanel.SetActive(false);
    }
}
