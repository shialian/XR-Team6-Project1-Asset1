using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowdness : MonoBehaviour
{
    public Transform[] sign = null;

    private int curTriggerIndex;
    private float curHP = 1.0f;
    private TurningManager parent;

    private void Start()
    {
        for (int i = 0; i < sign.Length; i++)
        {
            sign[i].gameObject.SetActive(false);
        }
        curTriggerIndex = transform.name[8] - '0' - 1;
        parent = transform.parent.GetComponent<TurningManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "MainPlayer")
        {
            switch (parent.state)
            {
                case "No Player":
                    parent.state = "Player In Turning";
                    break;
                case "Player In Turning":
                    parent.state = "Player Turned";
                    break;
            }
            for (int i = 0; i < sign.Length; i++)
            {
                // 玩家在轉彎前，頭上不會有提示球
                if (i != curTriggerIndex)
                    sign[i].gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainPlayer")
        {
            // 玩家道路中心後，原本頭上的提示球出現
            sign[curTriggerIndex].gameObject.SetActive(true);

            if (parent.state == "Player Turned")
            {
                parent.state = "Player Leave";
                parent.triggerLeaveIndex = curTriggerIndex;
                for (int i = 0; i < sign.Length; i++)
                {
                    sign[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
