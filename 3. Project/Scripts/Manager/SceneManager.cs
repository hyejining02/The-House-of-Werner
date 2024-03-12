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
    // �ε�ǰ� �ִ��� üũ�ϱ� ���� ����
    private bool loading = false;

    //�����ͻ󿡼� Ȯ���� �������� ����� ����
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

    // �ܺο��� ���� �Լ�
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
        // �񵿱�� ���� �ε��Ѵ�.
        AsyncOperation operation = UnitySceneManager.LoadSceneAsync(nextScene.ToString());

        bool state = false;
        while ( ! state )
        {
            if ( sceneList.ContainsKey(nextScene) )
            {
                sceneList[nextScene].Progress(operation.progress);
            }

            // ���� ����Ϸ� �Ǿ��ٸ� ó���Ѵ�.
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

    // �ȵ���̵�� ���� ��Ŀ���� ������ ��� ȣ��Ǵ� �Լ�
    private void OnApplicationFocus(bool focus)
    {
        
    }

    // �ȵ���̵��� ȨŰ�� ������ �� ȣ��Ǵ� �Լ�
    private void OnApplicationPause(bool pause)
    {
        
    }

    // ���α׷��� ����� �� Release�Լ��� ȣ��
    private void OnApplicationQuit()
    {

    }
}
