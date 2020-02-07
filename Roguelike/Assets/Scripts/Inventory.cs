using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;

    public GameObject[] slots;

    public int selectedSlot;

    private bool inActive;


    private void Start()
    {
        inActive = true;
        selectedSlot = 0; //[0-9]
    }

    private void Update()
    {
        if (Input.GetKeyUp("i"))
        {
            if (inActive == true)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].gameObject.SetActive(false);
                }
                inActive = false;
            }
            else if (inActive == false)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].gameObject.SetActive(true);
                }
                inActive = true;
            }
        }
        if (Input.GetKeyUp("left"))
        {
            selectedSlot--;
            if (selectedSlot < 0)
                selectedSlot = 9;
        }
        if (Input.GetKeyUp("right"))
        {
            selectedSlot++;
            if (selectedSlot > 9)
                selectedSlot = 0;
        }
        if (Input.GetKeyUp("r"))
        {
            if(inActive == true)
            {
                slots[selectedSlot].GetComponent<Slot>().DropItem(selectedSlot);
            }
        }
    }
}
