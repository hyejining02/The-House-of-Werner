using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public enum SceneType
{
    None,
    Logo,
    Title,
    About,
    Select,
    Game
}

public class SceneManager : TSingleton<SceneManager>
{
    // 로드되고 있는지 체크하기 위한 변수
    private bool loading = false;

    //에디터상에서 확인할 목적으로 만들어 놓음
    [SerializeField]
    private SceneType current = SceneType.None;

    private Dictionary<SceneType, Scene> sceneList = new Dictionary<SceneType, Scene>();


    public T AddScene<T> (SceneType sType, bool state = false ) where T : Scene
    {
        if ( ! sceneList.ContainsKey(sType))
        {
            T t = this.CreateObject<T>(transform);
            t.enabled = state;
            sceneList.Add(sType, t);
            return t;
        }

        sceneList[sType].enabled = state;
        return sceneList[sType] as T;
    }

    public void EnableScript(SceneType scene)
    {
        foreach( var pair in sceneList )
        {
            if ( pair.Key != scene )
            {
                pair.Value.enabled = false;
            }
            else
                pair.Value.enabled = true;
        }
    }

    // 외부에서 사용될 함수
    public void Enable(SceneType nextScene)
    {
        if ( sceneList.ContainsKey( nextScene))
        {
            if (loading)
                return;

            loading = true;
            EnableScript(nextScene);
            LoadAsync(nextScene);
        }
    }

    public void EnableDelay(float delayTime, SceneType nextScene)
    {
        if (loading)
            return;
        StartCoroutine(IEEnableDelay(delayTime, nextScene));
        
    }

    private IEnumerator IEEnableDelay( float delayTime, SceneType nextScene)
    {
        yield return new WaitForSeconds(delayTime);
        Enable(nextScene);
    }

    private void LoadAsync( SceneType nextScene)
    {
        StartCoroutine(IELoadAsync(nextScene));
    }
    
    private IEnumerator IELoadAsync(SceneType nextScene)
    {
        // 비동기로 씬을 로드한다.
        AsyncOperation operation = UnitySceneManager.LoadSceneAsync(nextScene.ToString());

        bool state = false;
        while ( ! state )
        {
            if ( sceneList.ContainsKey(nextScene) )
            {
                sceneList[nextScene].Progress(operation.progress);
            }

            // 씬이 변경완료 되었다면 처리한다.
            if ( operation.isDone)
            {
                state = true;
                if (sceneList.ContainsKey(current))
                    sceneList[current].Exit();

                if ( sceneList.ContainsKey(nextScene))
                    sceneList[nextScene].Enter();

                current = nextScene;
                loading = false;

            }

            yield return null;
        }
    }

    // 안드로이드등 에서 포커스가 맞춰질 경우 호출되는 함수
    private void OnApplicationFocus(bool focus)
    {
        
    }

    // 안드로이드등에서 홈키를 눌렀을 때 호출되는 함수
    private void OnApplicationPause(bool pause)
    {
        
    }

    // 프로그램이 종료될 때 Release함수를 호출
    private void OnApplicationQuit()
    {

    }
}
