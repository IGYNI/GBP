using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    public GameObject slotButton;

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         for(int i = 0; i < inventory.slots.Length; i++)
    //         {
    //             if (inventory.isFull[i] == false)
    //             {
    //                 inventory.isFull[i] = true;
    //                 Instantiate(slotButton, inventory.slots[i].transform);
    //                 inventory._inventory.Add(gameObject.tag);
    //                 Destroy(gameObject);
    //                 break;
    //             }
    //         }
    //     }
    // }
}
