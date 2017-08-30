using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField]
    protected string name;
    [SerializeField]
    protected int qty = 0;

    protected virtual int Value
    {
        get
        {
            return qty;
        }
        set
        {
            qty = value;
        }
    }

    public Item(string name, int qty)
    {
        this.name = name;
        Value = qty;
    }

    public int GetQty()
    {
        return qty;
    }

    public virtual void SetQty(int qty)
    {
        Value = qty;
    }

    public virtual void AddQty(int qty)
    {
        Value += qty;
    }

    public virtual void Decrease()
    {
        Value--;
    }

    public virtual void Increase()
    {
        Value++;
    }

    public virtual int GetQty(int q)
    {
        if (qty > q)
        {
            Value -= q;
            return q;
        }
        else
        {
            int toRet = q - qty;
            Value = 0;
            return toRet;
        }
    }

    public virtual bool Get(int qty)
    {
        if (Value >= qty)
        {
            Value -= qty;
            return true;
        }

        return false;
    }

    public virtual bool Get()
    {
        if (qty > 0)
        {
            Decrease();
            return true;
        }
        return false;
    }

    public string GetName()
    {
        return name;
    }
}
