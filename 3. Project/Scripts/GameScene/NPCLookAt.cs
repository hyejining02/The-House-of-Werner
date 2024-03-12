using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    Quest,
    Shop,
}

public class NPCLookAt : MonoBehaviour
{

    public NPCType npcType;
    public Transform player;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    if (player == null) return;

        //    NPCLookAt[] npcArray = GameObject.FindObjectsByType<NPCLookAt>(FindObjectsSortMode.InstanceID);
        //    List<NPCLookAt> list = new List<NPCLookAt>();
        //    List<float> distanceList = new List<float>();

        //    foreach (NPCLookAt npc in npcArray)
        //    {
        //        if (npc.player != null)
        //        {
        //            list.Add(npc);
        //            distanceList.Add(Vector3.Distance(player.position, npc.transform.position));
        //        }
        //    }

        //    NPCLookAt finded = null;


        //    if (list.Count == 0) return;

        //    int index = 0;
        //    float distance = distanceList[0];

        //    for (int i = 1; i < distanceList.Count; ++i)
        //    {
        //        if (distance > distanceList[i])
        //        {
        //            distance = distanceList[i];
        //            index = i;
        //        }
        //    }

        //    finded = list[index];

        //    if (finded == null)
        //        return;


        //    switch (finded.npcType)
        //    {
        //        case NPCType.Quest:
        //            break;

        //        case NPCType.Shop:
        //            break;
        //    }


        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Player") )
        {
            player = other.transform;
       
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
          
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if ( other.tag == "Player")
        {
            transform.LookAt(other.transform);
        }


    }

}
