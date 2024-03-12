using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private InputController inputController;
    private CharacterBase character;

    void Start()
    {
        inputController = GameObject.FindFirstObjectByType<InputController>();
        inputController?.Initialize();

        CharacterBase[] characters = GameObject.FindObjectsByType<CharacterBase>(FindObjectsSortMode.InstanceID);
        for( int i = 0; i < characters.Length; ++i)
        {
            characters[i].Initialize();
            if (characters[i].tag == "Player")
                character = characters[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
