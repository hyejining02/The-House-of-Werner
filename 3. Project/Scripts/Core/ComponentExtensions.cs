using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Xml.Linq;

// 확장함수
public static class ComponentExtensions
{
    // Component 클래스를 확장

    public static T Find<T>(this Component component, string path) where T : Component
    {
        Transform findTransform = component.transform.Find(path);

        if( findTransform != null )
        {
            return findTransform.GetComponent<T>();
        }

        return null;
    }

    public static T FindAndCall<T>(this Component component, string path, string function, System.Object[] objects = null) where T : Component
    {
        T t = component.Find<T>(path);

        if (t == null) 
            return t;

        Invoke<T>(t,function, objects);

        return t;
    }

    // Behavior클래스에 스크립트를 켜거나 끄는 enabled값이 있다.
    // 사용자가 만든 클래스는 MonoBehavior를 상속받고
    // 그 클래스에서는 SetEnable 함수를 사용해서 컴포넌트의 기능을 켜거나 끌 수 있다.
    public static void SetEnable( this Behaviour component, bool enable)
    {
        component.enabled = enable;
    }

    // 컴포넌트 클래스의 멤버로는 gameObject 속성값이 있다.
    // 이 속성값을 사용해서 게임오브젝트를 켜거나 끌 수 있도록 처리한다.
    public static void SetActive(this Component component, bool active)
    {
        component.gameObject.SetActive(active);
    }

    // 일반적인 정적함수와 확장함수의 차이
    // 확장함수의 경우 첫번째 매개변수가 this로 시작하고 확장시킬 클래스의 데이터타입을 넣어줘야 한다.
    
    // 확장함수
    public static T CreateObject<T>(this Component component, Transform parent = null) where T : Component
    {
        // 사용자가 지정한 컴포넌트의 데이터 타입으로 게임오브젝트의 이름을 설정한 후 생성된 게임오브젝트에 지정한 컴포넌트가 연결될 수 있도록 처리하는 코드
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));

        obj.transform.SetParent(parent);
        return obj.GetComponent<T>();
    }

    public static T CreateObjectAndCall<T>(this Component component, string function, Transform parent = null, System.Object[] objects = null) where T : Component
    {
        T t = component.CreateObject<T>(parent);

        Invoke<T>(t, function, objects);

        return t;
    }

    // 정적함수
    public static T CreateObject<T>( Transform parent = null) where T : Component
    {
        // 사용자가 지정한 컴포넌트의 데이터 타입으로 게임오브젝트의 이름을 설정한 후 생성된 게임오브젝트에 지정한 컴포넌트가 연결될 수 있도록 처리하는 코드
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));

        obj.transform.SetParent(parent);
        return obj.GetComponent<T>();
    }

    public static T CreateObjectAndCall<T>(string function, Transform parent = null, System.Object[] objects = null) where T : Component
    {
        T t = CreateObject<T>(parent);

        Invoke<T>(t, function, objects);

        return t;
    }

    // path값을 받아서 생성하는 함수 ( 편의성 ) 
    public static T Instantiate<T>(this Component component, string path) where T : Component
    {
        T t = Resources.Load<T>(path);

        if (t == null) return null;
        T newComponent = UnityEngine.Object.Instantiate(t);
        newComponent.name = t.name;

        return newComponent;
    }

    public static T Instantiate<T>(this Component component, string path, Transform parent) where T : Component
    {
        T newComponent = Instantiate<T>(component, path);
        if (newComponent == null ) return null;
        newComponent.transform.SetParent(parent);

        return newComponent;
    }

    public static T Instantiate<T>(this Component component, string path, Vector3 position, Transform parent = null) where T : Component
    {
        T t = component.Instantiate<T>(path);
        if (t == null) return null;

        t.transform.position = position;
        t.transform.rotation = Quaternion.identity;
        t.transform.SetParent(parent);
        return t;
    }

    public static T Instantiate<T>(this Component component, string path, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
    {
        T t = component.Instantiate<T>(path);
        if (t == null) return null;

        t.transform.position = position;
        t.transform.rotation = rotation;
        t.transform.SetParent(parent);
        return t;
    }

    public static void Invoke<T>(Component component, string function, System.Object[] objects = null)
    {
        Type tType = typeof(T);
        MethodInfo methodInfo = tType.GetMethod(function);
        methodInfo.Invoke(component, objects);
    }
    public static T InstantiateAndCall<T>(this Component component, string path, string function, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }
    public static T InstantiateAndCall<T>(this Component component, string path, string function, Transform parent, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path, parent);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }


    public static T InstantiateAndCall<T>(this Component component, string path, string function,
                                           Vector3 position, Transform parent = null, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path, position, parent);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }

    public static T InstantiateAndCall<T>(this Component component, string path, string function, 
                                           Vector3 position, Quaternion rotation, Transform parent = null, System.Object[] objects = null) where T : Component
    {

        T newComponent = Instantiate<T>(component, path, position, parent);
        if (newComponent == null) return null;
        Invoke<T>(newComponent, function, objects);

        return newComponent;
    }
}
