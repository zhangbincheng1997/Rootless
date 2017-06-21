using UnityEngine;

public class GroundController : MonoBehaviour
{
    private static GroundController _instance;
    public static GroundController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    [Header("地面组件")]
    public Transform groundTrans;
    [Header("虫子集合")]
    public Transform wormsTrans;

    private MTree nowTree;
    private Animator[] worms;
    private RectTransform groundRect;
    private float minX = 0; // 屏幕最小X值
    private float maxX = 0; // 屏幕最大X值
    private float nowX = 0; // 叶子当前X值

    void Start()
    {
        // 地面
        groundRect = this.GetComponent<RectTransform>();
        minX = groundRect.rect.width / 4;
        maxX = Screen.width - groundRect.rect.width / 4;
        nowX = groundRect.position.x;

        // 树苗
        GameObject go = Instantiate(Resources.Load(Consts.Sapling_Prefab) as GameObject);
        go.transform.SetParent(groundTrans);
        go.transform.localPosition = Vector3.zero;
        nowTree = go.GetComponent<MTree>();

        // 虫子
        worms = wormsTrans.GetComponentsInChildren<Animator>();
    }

    void Update()
    {
#if UNITY_EDITOR
        // 键盘输入
        nowX += Input.GetAxis("Horizontal") * Consts.Gravity_Pow;
        nowX = Mathf.Clamp(nowX, minX, maxX);
        groundRect.position = new Vector3(nowX, groundRect.position.y, 0);
#elif UNITY_ANDROID
        // 重力感应
        nowX += Input.acceleration.x * Consts.Gravity_Pow;
        nowX = Mathf.Clamp(nowX, 0, maxX);
        groundRect.position = new Vector3(nowX, groundRect.position.y, 0);
#endif
    }

    // 树木生长
    public void Grow(TreeType treeType)
    {
        nowTree.Growing();
        GameObject go = null;
        switch (treeType)
        {
            case TreeType.Sapling:
                go = Instantiate(Resources.Load(Consts.Sapling_Prefab) as GameObject);
                break;
            case TreeType.Small:
                go = Instantiate(Resources.Load(Consts.SmallTree_Prefab) as GameObject);
                break;
            case TreeType.Middle:
                go = Instantiate(Resources.Load(Consts.MiddleTree_Prefab) as GameObject);
                break;
            case TreeType.Big:
                go = Instantiate(Resources.Load(Consts.BigTree_Prefab) as GameObject);
                break;
            default:
                break;
        }
        go.transform.SetParent(groundTrans);
        go.transform.localPosition = Vector3.zero;
        nowTree = go.GetComponent<MTree>();
    }

    // 显示虫子
    public void Show()
    {
        nowTree.Shaking();
        wormsTrans.gameObject.SetActive(true);
        foreach (Animator worm in worms)
        {
            // 随机速度
            worm.speed = Random.Range(Consts.Worm_Min_Speed, Consts.Worm_Max_Speed);
        }
    }

    // 隐藏虫子
    public void Hide()
    {
        nowTree.Shaking();
        wormsTrans.gameObject.SetActive(false);
    }
}
