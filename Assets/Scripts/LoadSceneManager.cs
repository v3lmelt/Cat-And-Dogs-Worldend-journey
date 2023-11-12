using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneManager:Singleton<LoadSceneManager>
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
        Debug.Log("找猫狗");
        GameManager.Instance.FindCatAndDog();
        //if (GameManager.Instance.Cat != null) {
        //    Debug.Log("yes");                     //输出结果证明场景转换了之后 猫猫组件不是空的 说明还没有转换场景
        //}
        //else { Debug.Log("no"); }

        //测试证明      虽然找猫狗代码写在loadScene下面 但实际上虽然LoadScene执行完了 猫狗没有找到 说明场景此时却还没有转换 原因未知

       // StartCoroutine(Initialization());

    }//尼玛这里用携程还是有问题 我直接往GameManager的Updata里放FindCatAndDog就可以 
    //IEnumerator Initialization() //0.1秒后再执行
    //{
    //    yield return new WaitForSeconds(0.1f);
    //   GameManager.Instance.FindCatAndDog();
    //}
}