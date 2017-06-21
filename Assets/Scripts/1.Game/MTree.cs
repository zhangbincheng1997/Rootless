using UnityEngine;
using DG.Tweening;

public class MTree : MonoBehaviour
{
    private Transform[] leafs;
    private CanvasGroup canvasGroup;
    private Animator anim;

    void Start()
    {
        leafs = this.GetComponentsInChildren<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();
        anim = GetComponent<Animator>();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, Consts.Tree_Grow_Time);

        InvokeRepeating("Falling", 0, Consts.Tree_Leaf_Time);
    }

    // 接口 生长
    public void Growing()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, Consts.Tree_Grow_Time)
            .OnComplete(delegate { Destroy(this.gameObject); });
    }

    // 接口 摇摆
    public void Shaking()
    {
        anim.SetTrigger("Shake");
    }

    // 自动 落叶
    private void Falling()
    {
        // 叶子类型
        int r1 = Random.Range(0, 3);
        GameObject go = GameObject.Instantiate(Resources.Load(Consts.Leaf_Prefab + r1) as GameObject);
        // 叶子位置
        int r2 = Random.Range(0, leafs.Length);
        go.transform.SetParent(leafs[r2].transform);
        go.transform.localPosition = Vector3.zero;
    }
}
