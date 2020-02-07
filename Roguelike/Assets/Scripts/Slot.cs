using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public void DropItem(int selectedSlot)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpawnItem>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
            inventory.isFull[selectedSlot] = false;
        }
    }
}
