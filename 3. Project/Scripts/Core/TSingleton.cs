using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̱����̶�
// 1) �����ڰ� ����� ������ �־�� �Ѵ�.
// 2) �ѹ��� ������ �� �־�� �Ѵ�.
// 3) ���������� Ȱ���Ѵ�.
public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    private static T instance;

    protected TSingleton()
    {

    }

    public virtual void Initialize()
    {


    }

    // static ���� ���� : ���α׷��� �����ɶ����� ������ �� �ִ� ���� ( �Ϲ����� ���������� ���̰� �ִ� ) 
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

                // ���� ����Ǵ��� ������ ���ӿ�����Ʈ�� �ı����� �ʵ��� ó��
                DontDestroyOnLoad( instance.gameObject );
            }

            return instance;
        }
    }
}
