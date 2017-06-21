using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMgr : UnitySingleton<SceneMgr>
{
    // 画布的transform
    private Transform _canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (_canvasTransform == null)
            { _canvasTransform = GameObject.Find("Canvas").transform; }
            return _canvasTransform;
        }
    }

    private Text loadingProgress;
    private Slider loadingBar;
    private Transform loadingIcon;

    public void LoadScene(int sceneId)
    {
        // 初始化
        GameObject go = Instantiate(Resources.Load(Consts.LoadingScene_Prefab) as GameObject);
        go.transform.SetParent(CanvasTransform);
        go.transform.localPosition = Vector3.zero;
        // 获取组件
        loadingProgress = GameObject.Find(Consts.Loading_Progress).GetComponent<Text>();
        loadingBar = GameObject.Find(Consts.Loading_Bar).GetComponent<Slider>();
        loadingIcon = GameObject.Find(Consts.Loading_Icon).transform;
        // 开启协程
        StartCoroutine("LoadNormalScene", sceneId);
    }

    // 协程加载场景
    IEnumerator LoadNormalScene(int sceneId)
    {
        int startProgress = 0;
        int displayProgress = startProgress;
        int toProgress = startProgress;

        // 异步加载场景
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneId);

        // 控制异步加载的场景暂时不进入
        op.allowSceneActivation = false;

        /*
            progress的取值范围在0.1 - 1之间，但是不会等于1
            即progress可能在0.9的时候就会直接进入新场景
            所以需要分别控制两种进度0.1 - 0.9和0.9 - 1
        */

        // 计算读取的进度
        while (op.progress < 0.9f)
        {
            toProgress = startProgress + (int)(op.progress * 100);
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetProgress(displayProgress);
                yield return null;
            }
            yield return null;
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetProgress(displayProgress);
            yield return null;
        }

        // 激活场景
        op.allowSceneActivation = true;
    }

    // 设置进度
    void SetProgress(int progress)
    {
        loadingProgress.text = progress.ToString() + " %";
        loadingBar.value = progress * 0.01f;
        loadingIcon.localPosition = new Vector3(progress * 10 - 500, 0, 0);  // (-500, 500)
    }
}
