using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public float damageRatio;
    public Slider slider;
    public Image fillImage;
    public float timeForSoul;


    private Inventory inventory;
    private bool activeSoul;
    private Color color1;
    private Color color2;
    private int changeColorTime = 5;
    void Start()
    {
        maxHP = 9f;
        currentHP = 5f;
        damageRatio = 1;
        DisplayHP();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        activeSoul = false;
        color1 = new Color(255, 255, 255, 1f);
        color2 = new Color(255, 255, 255, 0f);
        fillImage.color = color1;
    }

    private void Update()
    {
        if (currentHP / maxHP < 0.15f)
        {
            if (changeColorTime <= 0)
            {
                if (fillImage.color == color1)
                {
                    fillImage.color = color2;
                }
                else if (fillImage.color == color2)
                {
                    fillImage.color = color1;
                }
                changeColorTime = 10;
            }
            else
            {
                changeColorTime--;
            }
        }
        if (Input.GetKeyUp("1")) 
        {            
            RecoveryHP();
        }
        if (Input.GetKeyUp("2"))
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().timeForScroll <= 0 && timeForSoul <= 0)
            UseSoul();
        }
    }

    private void FixedUpdate()
    {
        if(activeSoul == true && timeForSoul > 0f)
        {
            timeForSoul -= Time.deltaTime;
        }
        else
        {
            activeSoul = false;
            damageRatio = 1f;
        }
    }
    //получение урона и снижение его под бафами свитка
    public void TakingDamage(float damage)
    {
        currentHP = currentHP - damage * damageRatio;
        DisplayHP();
    }

    //восполнение хп
    public void RecoveryHP()
    {
        for (int i = 0; i < inventory.slots.Length; i++) //inventory.slots.Length
        {
            if(inventory.slots[i].transform.childCount > 0)
            {
                if (inventory.slots[i].transform.GetChild(0).CompareTag("HealthPotion"))
                {
                    currentHP += 6;
                    if (currentHP > maxHP)
                    {
                        currentHP = maxHP;
                    }
                    inventory.isFull[i] = false;
                    foreach (Transform t in inventory.slots[i].transform)
                    {
                        Destroy(t.gameObject);
                    }
                    break;
                }

            }
            else
            {
                continue;
            }
            
        }
        DisplayHP();
    }

    //использование души для уменьшения дамага
    public void UseSoul()
    {
        for (int i = 0; i < inventory.slots.Length; i++) //inventory.slots.Length
        {
            if (inventory.slots[i].transform.childCount > 0)
            {
                if (inventory.slots[i].transform.GetChild(0).CompareTag("Soul"))
                {
                    damageRatio = 0.4f;
                    activeSoul = true;
                    timeForSoul = 15f;
                    inventory.isFull[i] = false;
                    foreach (Transform t in inventory.slots[i].transform)
                    {
                        Destroy(t.gameObject);
                    }
                    break;
                }
            }
            else
            {
                continue;
            }

        }
    }
    private void DisplayHP()
    {
        float HPSlider = currentHP / maxHP;

        if(HPSlider > 0.10f)
        {
            //fillImage.color = color1;
            slider.value = HPSlider;
        }

        else if (HPSlider <= 0.10f)
        {
            if (HPSlider < 0.05f)
            {
                slider.value = HPSlider;
            }
            else slider.value = 0.05f;
        }

        
    }

    public void LvlHPUp()
    {
        maxHP = 9 * LevelGenerator.LVL;
        currentHP += 9f;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        DisplayHP();
    }
}
