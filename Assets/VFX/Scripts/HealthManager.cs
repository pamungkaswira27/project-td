using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image damageScreen;
    public Image bloodSplash;
    public Slider healthBar;
    public int life = 10;
    int maxLife;
    Color alphaColorBldSplash;
    Color alphaColorDmgScreen;
    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
        healthBar.maxValue = maxLife;
        alphaColorBldSplash = bloodSplash.color;
        alphaColorDmgScreen = damageScreen.color;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = life;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage();
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            Heal();
        }
    }

    private void TakeDamage() 
    {
        if (life > 0)
        {
            life--;
        }

        alphaColorBldSplash.a += .1f;
        bloodSplash.color = alphaColorBldSplash;

        if (life < 5) 
        {         
        alphaColorDmgScreen.a += .1f;
        damageScreen.color = alphaColorDmgScreen;
        }
    }

    private void Heal() 
    {
        if (life < maxLife) {
            life++;

            alphaColorBldSplash.a -= .1f;
            bloodSplash.color = alphaColorBldSplash;
        }
        if (life > 4) 
        {         
            alphaColorDmgScreen.a -= .1f;
            damageScreen.color = alphaColorDmgScreen;
        }
    }
}
