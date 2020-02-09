using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP;//максимальное хп без шмотки
    public float currentMaxHP;//максимальное хп под бафом шмотки
    public float currentHP;//текущее

    public float maxDamageRatio;//максимальный коеф без шмотки    
    public float currentMaxDamageRatio;//текущий под бафом шмотки
    public float currentDamageRatio;//текущий коеф дамага


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
        maxHP = 16f;
        currentHP  = currentMaxHP = maxHP;
        maxDamageRatio = currentMaxDamageRatio = 1;
        DisplayHP();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        activeSoul = false;
        color1 = new Color(255, 255, 255, 1f);
        color2 = new Color(255, 255, 255, 0f);
        fillImage.color = color1;
    }

    private void Update()
    {
        if (currentHP / currentMaxHP < 0.15f)
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
            currentDamageRatio = currentMaxDamageRatio;
        }
    }
    //получение урона и снижение его под бафами свитка
    public void TakingDamage(float damage)
    {
        currentHP = currentHP - damage * currentDamageRatio;
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
                    currentHP += 10;
                    if (currentHP > currentMaxHP)
                    {
                        currentHP = currentMaxHP;
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
                    currentDamageRatio = currentMaxDamageRatio * 0.4f;
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
        float HPSlider = currentHP / currentMaxHP;

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
        bool Is = false;
        if (currentMaxHP > maxHP)
            Is = true;
        maxHP = 16 * LevelGenerator.LVL;
        //тут еще что то нужно, типо когда на некст лвл переход, и есть активная шмотка, то и карентМакс нужно увеличить
        //пошаманить потом
        currentMaxHP = maxHP;
        if (Is)
        {
            AmuletBuff.SetBuff(0.3f, 0, 0);
        }
        currentHP += 16f;
        if (currentHP > currentMaxHP)
        {
            currentHP = currentMaxHP;
        }
        DisplayHP();
    }

    public void HPBuff(float hpBuff)
    {
        currentMaxHP = maxHP * (1 + hpBuff);
        DisplayHP();
    }
    public void RatioBuff(float ratioBuff)
    {
        currentMaxDamageRatio = maxDamageRatio * ratioBuff;
        if (currentHP > currentMaxHP)
        {
            currentHP = currentMaxHP;
        }
        DisplayHP();
    }
}
