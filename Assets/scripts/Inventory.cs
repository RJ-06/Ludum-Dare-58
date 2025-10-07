using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory;
    [SerializeField] int maxInventorySize;
    public int selectedItemInd;
    public GameObject itemHeld;

    private void Update()
    {
        for (int i = 0; i < inventory.Count; i++) 
        {
            if (inventory[i].GetComponent<Health>().GetHealth() <= 0) 
            {
                Destroy(inventory[i]);
                inventory.RemoveAt(i);
            }
        }
    }

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
        if (ind < inventory.Count)
        {
            selectedItemInd = ind;
            //TODO - add some effects or smth when you choose an item ie particle effects
            itemHeld = inventory[selectedItemInd];
            Debug.Log(selectedItemInd);
        }
    }

    public void OnPrevious()
    {
        if (inventory.Count == 0)
        {
            return;
        }
        selectedItemInd--;
        if (selectedItemInd < 0)
        {
            selectedItemInd = inventory.Count - 1;
        }
        ChangeSelectedItem(selectedItemInd);
    }

    public void OnNext()
    {
        if (inventory.Count == 0)
        {
            return;
        }
        selectedItemInd++;
        if (selectedItemInd >= inventory.Count)
        {
            selectedItemInd = 0;
        }
        ChangeSelectedItem(selectedItemInd);
    }

    public void DebugInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log("item " + i + ": " + inventory[i].name);
        }
    }
}
