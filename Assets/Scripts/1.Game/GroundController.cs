using UnityEngine;

public class GroundController : MonoBehaviour
{
    private static GroundController _instance;
    public static GroundController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    [Header("地面组件")]
    public Transform groundTrans;
    [Header("虫子集合")]
    public Transform[] worms;

    private GameObject nowTree;
    private RectTransform groundRect;
    private float minX = 0; // 屏幕最小X值
    private float maxX = 0; // 屏幕最大X值
    private float nowX = 0; // 叶子当前X值

    void Start()
    {
        groundRect = this.GetComponent<RectTransform>();
        minX = groundRect.rect.width / 4;
        maxX = Screen.width - groundRect.rect.width / 4;
        nowX = groundRect.position.x;

        // 新生树苗
        nowTree = Instantiate(Resources.Load(Consts.Sapling_Prefab) as GameObject);
        nowTree.transform.SetParent(groundTrans);
        nowTree.transform.localPosition = Vector3.zero;
    }

    void FixedUpdate()
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
        nowTree.GetComponent<MTree>().Growing();
        switch (treeType)
        {
            case TreeType.Sapling:
                nowTree = Instantiate(Resources.Load(Consts.Sapling_Prefab) as GameObject);
                break;
            case TreeType.Small:
                nowTree = Instantiate(Resources.Load(Consts.SmallTree_Prefab) as GameObject);
                break;
            case TreeType.Middle:
                nowTree = Instantiate(Resources.Load(Consts.MiddleTree_Prefab) as GameObject);
                break;
            case TreeType.Big:
                nowTree = Instantiate(Resources.Load(Consts.BigTree_Prefab) as GameObject);
                break;
            default:
                break;
        }
        nowTree.transform.SetParent(groundTrans);
        nowTree.transform.localPosition = Vector3.zero;
    }

    // 显示虫子
    public void Show()
    {
        nowTree.GetComponent<MTree>().Shaking();
        foreach (Transform worm in worms)
        {
            worm.gameObject.SetActive(true);
            // 随机速度
            worm.GetComponent<Animator>().speed = Random.Range(0.5f, 1.5f);
        }
    }

    // 隐藏虫子
    public void Hide()
    {
        nowTree.GetComponent<MTree>().Shaking();
        foreach (Transform worm in worms)
        {
            worm.gameObject.SetActive(false);
        }
    }
}
