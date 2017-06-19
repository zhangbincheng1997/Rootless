using UnityEngine;
using UnityEngine.UI;

public class DayController : MonoBehaviour
{
    private static DayController _instance;
    public static DayController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    // 计时器
    private float timer = 0;
    // 是否更新
    private bool isUpdate = true;

    void Update()
    {
        if (!isUpdate) { return; }

        timer += Time.deltaTime;
        if (timer >= Consts.Night_Time)
        {
            isUpdate = false;
            timer = 0;
            Day();
        }
    }

    public void NextDay()
    {
        isUpdate = true;
        Night();
    }

    void Day()
    {
        MessageController.Instance.ShowMessage(Consts.Message_Day);
        this.GetComponent<CanvasGroup>().alpha = 0;
        GameController.Instance.gameState = GameState.Start;
    }

    void Night()
    {
        MessageController.Instance.ShowMessage(Consts.Message_Night);
        this.GetComponent<CanvasGroup>().alpha = 1;
    }
}
