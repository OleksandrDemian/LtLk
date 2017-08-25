using System.Collections.Generic;

public class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Item exist = GetItem(item.GetName());

        if (exist == null)
            items.Add(item);
        else
            exist.AddQty(item.GetQty());
    }

    public void AddItem(Item item, bool ignoreExist)
    {
        if (ignoreExist)
            items.Add(item);
        else
            AddItem(item);
    }

    public Item GetItem(string name)
    {
        int size = items.Count;
        for (int i = 0; i < size; i++)
        {
            UnityEngine.Debug.Log("Looking for: " + name + " now: " + items[i].GetName());
            if (items[i].GetName() == name)
                return items[i];
        }
        
        Item item = new Item(name, 0);
        items.Add(item);
        return item;
    }

    public void RemoveItem(string name)
    {
        Item item = GetItem(name);
        if (item != null)
            items.Remove(item);
    }

    public bool Has(string name)
    {
        int size = items.Count;
        for (int i = 0; i < size; i++)
        {
            if (items[i].GetName() == name)
                return true;
        }
        return false;
    }

    public int Size()
    {
        return items.Count;
    }
}
