using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverToggle : MonoBehaviour
{
    public GameObject sign;
    public GameObject curtain;
    public float time;
    public string level;
    public HealthBarDrop fatherHealth;
    public void ToggleGameOver(GameObject image)
    {
        
        if(fatherHealth.GetHealthBarValue() <= 0.0f)
        {
            image.SetActive(true);
            curtain.SetActive(true);
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(level);
            }
        }
        else
        {
            image.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ToggleGameOver(sign);
    }
}
