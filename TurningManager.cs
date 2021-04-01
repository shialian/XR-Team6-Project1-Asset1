using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningManager : MonoBehaviour
{
    public string state;
    public int triggerLeaveIndex;
    public Transform[] sign;
    public HealthBarDrop fatherHealth;

    private int[] count;
    private float curHP = 1.0f;

    private void Start()
    {
        state = "No Player";
        count = new int[sign.Length];
        for(int i =0; i < sign.Length; i++)
        {
            count[i] = 0;
        }
        triggerLeaveIndex = -1;
    }

    private void Update()
    {
        if(state == "Player In Turning")
        {
            for (int i = 0; i < sign.Length; i++)
            {
                int index = sign[i].name[5] - '0' - 1;
                count[i] = 0;
                count[i] = CarsOnTheRoad(sign[i], index);
                ChangeSignColor(sign[i], count[i]);
            }
        }
        else if(state == "Player Leave")
        {
            float turnDamage = count[triggerLeaveIndex] / 100.0f;
            float actulDamage = 0.15f + turnDamage;
            curHP -= actulDamage;
            curHP = Mathf.Max(0f, curHP);
            fatherHealth.SetHealthBarValue(curHP);
            state = "No Player";
        }
    }

    private int CarsOnTheRoad(Transform signObject, int index)
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("CarPrefab");
        Vector3[] signObjectDirection = {
            Vector3.back,
            Vector3.left,
            Vector3.forward,
            Vector3.right
        };
        int carCount = 0;

        if (index % 2 == 0)
        {
            string axis = "x";
            for (int j = 0; j < cars.Length; j++)
            {
                Vector3 carPosition = cars[j].transform.position;
                Vector3 signObjectPosition = signObject.position;
                Vector3 checkingDirection = signObjectDirection[index];
                if (isOnRoad(carPosition, signObjectPosition, axis) && isOnDirection(carPosition, signObjectPosition, checkingDirection))
                {
                    carCount++;
                }
            }
            
        }
        else
        {
            string axis = "z";

            for (int j = 0; j < cars.Length; j++)
            {
                Vector3 carPosition = cars[j].transform.position;
                Vector3 signObjectPosition = signObject.position;
                Vector3 checkingDirection = signObjectDirection[index];
                if (isOnRoad(carPosition, signObjectPosition, axis) && isOnDirection(carPosition, signObjectPosition, checkingDirection))
                    carCount++;
            }
        }
        return carCount;
    }

    private bool isOnRoad(Vector3 car, Vector3 signObject, string axis)
    {
        if (axis == "x")
            return Mathf.Abs(car.x - signObject.x) < 4f;
        else
            return Mathf.Abs(car.z - signObject.z) < 4f;
    }

    private bool isOnDirection(Vector3 car, Vector3 signObject, Vector3 checkingDrection)
    {
        return Vector3.Dot(car - signObject, checkingDrection) > 0;
    }

    private void ChangeSignColor(Transform signObject, int count)
    {
        if (count <= 5)
            signObject.GetComponent<Renderer>().material.color = Color.green;
        else if (count <= 10)
            signObject.GetComponent<Renderer>().material.color = Color.yellow;
        else
            signObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
