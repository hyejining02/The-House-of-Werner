using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroup : MonoBehaviour
{
    private new Camera camera;

    public Vector3 position = new Vector3(0, -1.63f, 0.53f);
    public Vector3 eulerAngle = new Vector3(0, 180, 0);
    public Vector3 scale = Vector3.one;

    private int characterLayer = 0;




    public void Initialize()
    {
        characterLayer = LayerMask.NameToLayer("CharacterIcon");
        camera = GetComponentInChildren<Camera>();
        camera.transform.localPosition = Vector3.zero;
    }

    public void Create(UnityEngine.Object prefab, RenderTexture renderTexture)
    {
        if (prefab == null) return;

        GameObject newCharacter = Instantiate(prefab, transform) as GameObject;
        newCharacter.transform.localPosition = position;
        newCharacter.transform.localRotation = Quaternion.Euler(eulerAngle);
        newCharacter.transform.localScale = scale;

        // 보여지는 렌더러의 레이어를 변경
        SkinnedMeshRenderer skinned = newCharacter.GetComponentInChildren<SkinnedMeshRenderer>();
        skinned.gameObject.layer = characterLayer;
        camera.targetTexture = renderTexture;

        // 애니메이터의 기능을 끈다.
        Animator animator = newCharacter.GetComponent<Animator>();
        animator.enabled = false;
    }

}
