using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDrop : MonoBehaviour
{
    private static Image HealthBarImage;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
        Debug.Log(HealthBarImage.fillAmount);
        if (HealthBarImage.fillAmount < 0.2f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (HealthBarImage.fillAmount < 0.4f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else if (HealthBarImage.fillAmount < 0.6f)
        {
            SetHealthBarColor(Color.green);
        }
        else
            SetHealthBarColor(Color.blue);
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }
}
