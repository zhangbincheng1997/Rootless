using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum WeatherType
{
    Rain, // 雨
    Wind // 风
}

public class Weather : MonoBehaviour
{
    [Header("天气类型")]
    public WeatherType weatherType;

    void Update()
    {
        this.transform.Translate(Vector3.down * Time.deltaTime * Consts.Weather_Fall_Speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 触碰到地面
        if (other.tag.Equals(Consts.Ground_Tag))
        {
            // 重置位置
            this.transform.position = Vector3.zero;
            // 将物体放回对象池
            PoolMgr.Instance.Unspawn(this.gameObject);
        }
        // 增加滋润值
        else if (weatherType == WeatherType.Rain && other.tag.Equals(Consts.Tree_Tag))
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            MessageController.Instance.ShowMessage(Consts.Message_Cool);
            GameController.Instance.RichChange(Consts.Rain_Reward);
            StartCoroutine("Hide");
        }
        // 减少健康值
        else if (weatherType == WeatherType.Wind && other.tag.Equals(Consts.Tree_Tag))
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            MessageController.Instance.ShowMessage(Consts.Message_Ouch);
            GameController.Instance.HealthChange(Consts.Wind_Punish);
            StartCoroutine("Hide");
        }
    }

    IEnumerator Hide()
    {
        this.GetComponent<Image>().CrossFadeAlpha(0, Consts.Weather_Hide_Time, true);
        yield return new WaitForSeconds(Consts.Weather_Hide_Time);
        // 将物体放回对象池
        PoolMgr.Instance.Unspawn(this.gameObject);
    }
}
