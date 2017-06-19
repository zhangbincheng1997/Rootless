using UnityEngine;

public class Leave : MonoBehaviour
{
    float timer = 0.0f; // 计时器
    float time = 0.5f; // 频率
    Vector3 tarDir = Vector3.down;
    Vector3 dir = Vector3.down;

    void Start()
    {
        Destroy(this.gameObject, 2.0f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            float randomX = Random.Range(-2f, 2f);
            tarDir.x = randomX;
            timer = 0.0f;
        }
        dir = Vector3.Lerp(dir, tarDir, Time.deltaTime * 5f);
        transform.Translate(dir * Time.deltaTime * 40f);
        transform.RotateAround(transform.position, Vector3.forward, 1f * Time.deltaTime);
    }
}
