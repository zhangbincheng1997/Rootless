using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MTree : MonoBehaviour
{
    public List<GameObject> leaveList; // 树叶集合
    public GameObject leavePrefab; // 树叶预设体
    public GameObject leavePrefab_1; // 树叶预设体
    public GameObject leavePrefab_2; // 树叶预设体

    private CanvasGroup canvasGroup;
    private Animator anim;
    private float timer = 0.0f; // 计时器
    private float time = 3.0f; // 频率

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, Consts.Grow_Time);
    }

    void Update()
    {
        LeaveFalling();
    }

    // 接口 生长
    public void Growing()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, Consts.Grow_Time)
            .OnComplete(delegate { Destroy(this.gameObject); });
    }

    // 接口 摇摆
    public void Shaking()
    {
        anim.SetTrigger("Shake");
    }

    // 自动 落叶
    private void LeaveFalling()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0.0f;
            Transform leavePos = leaveList[Random.Range(0, 2)].transform;
            GameObject _leave = null;
            switch (Random.Range(0, 3))
            {
                case 0:
                    _leave = GameObject.Instantiate(leavePrefab, leavePos.position, Quaternion.identity);
                    break;
                case 1:
                    _leave = GameObject.Instantiate(leavePrefab_1, leavePos.position, Quaternion.identity);
                    break;
                case 2:
                    _leave = GameObject.Instantiate(leavePrefab_2, leavePos.position, Quaternion.identity);
                    break;
            }
            if (_leave != null)
                _leave.transform.SetParent(transform);
        }
    }
}
