using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;

    public GameObject[] slots;

    public int selectedSlot;

    private bool inActive;

    public Sprite[] sprites = new Sprite[2];

    public Image imageInfo;
    public Text itemTextInfo;

    private void Start()
    {
        inActive = true;
        selectedSlot = 0; //[0-9]
        GetTextInfo();
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
            slots[selectedSlot].GetComponent<Image>().sprite = sprites[0];
            selectedSlot--;
            if (selectedSlot < 0)
                selectedSlot = 7;
            slots[selectedSlot].GetComponent<Image>().sprite = sprites[1];
            //отображение инфы
            GetTextInfo();
        }
        if (Input.GetKeyUp("right"))
        {
            slots[selectedSlot].GetComponent<Image>().sprite = sprites[0];
            selectedSlot++;
            if (selectedSlot > 7)
                selectedSlot = 0;
            slots[selectedSlot].GetComponent<Image>().sprite = sprites[1];
            //отображение инфы
            GetTextInfo();
        }
        if (Input.GetKeyUp("up"))
        {
            if(inActive == true)
            {
                slots[selectedSlot].GetComponent<Slot>().DropItem(selectedSlot);
            }
        }
        //одеть\cнять итем
        if (Input.GetKeyUp("down"))
        {
            if (inActive == true)
            {
                if(selectedSlot == 7)
                {
                    //если 7 занят и мы тыкаем то снять итем
                    if(isFull[7])
                    slots[selectedSlot].GetComponent<Slot>().PutOutItem(7);
                }
                else
                {
                    slots[selectedSlot].GetComponent<Slot>().PutOnItem(selectedSlot);
                }

            }
        }
    }
    void GetTextInfo()
    {
        var position = imageInfo.transform.position;
        position.x = (slots[selectedSlot].transform.position.x);
        imageInfo.transform.position = position;
        itemTextInfo.text = slots[selectedSlot].GetComponent<Slot>().GetInfo();
    }
}
