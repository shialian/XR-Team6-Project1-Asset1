using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public HealthBarDrop fatherHealth;
    // Start is called before the first frame update
    void Start()
    {
       fatherHealth.SetHealthBarValue(1.0f);
    }

    // Update is called once per frame
}
