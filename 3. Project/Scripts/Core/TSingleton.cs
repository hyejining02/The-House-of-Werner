using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글턴이란
// 1) 생성자가 비공개 영역에 있어야 한다.
// 2) 한번만 생성될 수 있어야 한다.
// 3) 정적변수를 활용한다.
public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    private static T instance;

    protected TSingleton()
    {

    }

    public virtual void Initialize()
    {


    }

    // static 정적 영역 : 프로그램이 구동될때부터 접근할 수 있는 영역 ( 일반적인 지역변수와 차이가 있다 ) 
    public static T Instance
    {
        get
        {
            if ( instance == null )
            {
                //GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                //instance = obj.GetComponent<T>();

                instance = ComponentExtensions.CreateObject<T>();
                instance.Initialize();

                // 씬이 변경되더라도 생성된 게임오브젝트가 파괴되지 않도록 처리
                DontDestroyOnLoad( instance.gameObject );
            }

            return instance;
        }
    }
}
