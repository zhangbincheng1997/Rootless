using UnityEngine;
using DG.Tweening;

public class Cloud : MonoBehaviour
{
    public RectTransform cloud_1;
    public RectTransform cloud_2;

    void Start()
    {
        // DOMoveX endValue duration
        // SetLoops loops loopType
        cloud_1.DOMoveX(1000, 20).SetLoops(-1, LoopType.Restart);
        cloud_2.DOMoveX(-1000, 40).SetLoops(-1, LoopType.Restart);
    }
}
