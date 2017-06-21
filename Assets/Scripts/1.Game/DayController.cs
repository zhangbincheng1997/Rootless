using UnityEngine;
using DG.Tweening;

public class DayController : MonoBehaviour
{
    private static DayController _instance;
    public static DayController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    private CanvasGroup canvasGroup;
    void Start() { canvasGroup = this.GetComponent<CanvasGroup>(); }

    public void NextDay()
    {
        // 夜晚
        MessageController.Instance.ShowMessage(Consts.Message_Night);
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, Consts.Night_Time)
            .OnComplete(delegate { Day(); });
    }

    void Day()
    {
        // 白天
        MessageController.Instance.ShowMessage(Consts.Message_Day);
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, Consts.Night_Time)
            .OnComplete(delegate { GameController.Instance.gameState = GameState.Event; });
    }
}
