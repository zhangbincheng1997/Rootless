using UnityEngine;

public class MLeaf : MonoBehaviour
{
    private Vector3 tarDir = Vector3.down;
    private Vector3 dir = Vector3.down;

    void Start() { Destroy(this.gameObject, Consts.Leaf_ExTime); }

    float timer = 0.0f; // 计时器
    float time = Consts.Leaf_Time; // 叶子偏移时间
    float offset = Consts.Leaf_Offset; // 叶子偏移量
    float smooth = Consts.Leaf_Smooth; // 叶子平滑值
    float speed = Consts.Leaf_Speed; // 叶子速度
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            float randomX = Random.Range(-offset, offset);
            tarDir.x = randomX;
            timer = 0.0f;
        }
        dir = Vector3.Lerp(dir, tarDir, Time.deltaTime * smooth);
        transform.Translate(dir * Time.deltaTime * speed);
        transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime);
    }
}
