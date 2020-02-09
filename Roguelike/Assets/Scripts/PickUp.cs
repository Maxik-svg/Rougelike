using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;

    public GameObject itemButton;

    public string type;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }


    public void OnTriggerStay2D(Collider2D other) //PickUpToSlot OnTriggerEnter2D
    {
        if (Input.GetKeyUp("e"))
        {
            if (other.CompareTag("Player"))
            {
                if (type == "consumable")
                {
                    for (int i = 0; i < inventory.slots.Length - 1; i++)
                    {
                        if (inventory.isFull[i] == false)
                        {
                            //добавляем
                            inventory.isFull[i] = true;
                            Instantiate(itemButton, inventory.slots[i].transform, false);
                            Destroy(gameObject);
                            break;
                        }
                    }
                }
                else if (type == "amulet")
                {
                    if (inventory.isFull[7] == false)
                    {
                        inventory.isFull[7] = true;
                        Instantiate(itemButton, inventory.slots[7].transform, false);
                        //inventory.slots[7].GetComponent<Slot>().PutOnItem(7);
                        foreach (Transform child in inventory.slots[7].transform)
                        {
                            if (child.CompareTag("BAmulet"))
                            {
                                AmuletBuff.SetBuff(0, 0.3f, 1);
                            }
                            if (child.CompareTag("GAmulet"))
                            {
                                AmuletBuff.SetBuff(0.3f, 0, 1);
                            }
                            if (child.CompareTag("YAmulet"))
                            {
                                AmuletBuff.SetBuff(0, 0, 0.77f);
                            }
                            Destroy(gameObject);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < inventory.slots.Length - 1; i++)
                        {
                            if (inventory.isFull[i] == false)
                            {
                                //добавляем
                                inventory.isFull[i] = true;
                                Instantiate(itemButton, inventory.slots[i].transform, false);
                                Destroy(gameObject);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

    
