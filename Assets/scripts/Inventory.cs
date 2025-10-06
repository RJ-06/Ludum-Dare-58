using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory;
    [SerializeField] int maxInventorySize;
    public int selectedItemInd;
    public GameObject itemHeld;

    public void AddObjectToInventory(GameObject g)
    {
        if (inventory.Count >= maxInventorySize) return;

        inventory.Add(g);

        if (inventory.Count == 1)
        {
            ChangeSelectedItem(0);
        }
        //TODO - get the icon of the object to display it in inventory (if inventory ui is minecraft inspired)
    }

    public void RemoveObjectFromInventory(int index) 
    {
        if (index < inventory.Count) 
        {
            inventory.RemoveAt(index);
        }
    }

    void ChangeSelectedItem(int ind) 
    { 
        if(ind < inventory.Count) 
        {
            selectedItemInd = ind;
            //TODO - add some effects or smth when you choose an item ie particle effects
            itemHeld = inventory[selectedItemInd];
        }
    }

    public void DebugInventory() 
    {
        for (int i = 0; i < inventory.Count; i++) 
        {
            Debug.Log("item " + i + ": " + inventory[i].name);
        }
    }
}
