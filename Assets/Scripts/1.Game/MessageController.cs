using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    private static MessageController _instance;
    public static MessageController Instance { get { return _instance; } }
    void Awake() { _instance = this; }

    // 外界调用
    public void ShowMessage(string content)
    {
        this.GetComponent<Text>().text = content;
        StartCoroutine("Hide");
    }

    // 内部协程
    IEnumerator Hide()
    {
        this.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(Consts.Message_Time);
        this.GetComponent<Text>().enabled = false;
    }
}
