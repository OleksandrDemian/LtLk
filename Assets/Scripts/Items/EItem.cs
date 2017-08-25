public delegate void OnItemQuantityChange(int qty);

public class EItem : Item
{
    private OnItemQuantityChange listener;

    protected override int Value
    {
        get
        {
            return base.Value;
        }
        set
        {
            base.Value = value;
            if (listener != null)
                listener(qty);
        }
    }

    public EItem(string name, int qty) : base(name, qty)
    {

    }

    public void SetListener(OnItemQuantityChange listener)
    {
        this.listener = listener;
    }
    /*
    public override void AddQty(int qty)
    {
        base.AddQty(qty);
        if (listener != null)
            listener(this.qty);
    }

    public override void Decrease()
    {
        base.Decrease();
        if (listener != null)
            listener(qty);
    }

    public override void Increase()
    {
        base.Increase();
        if (listener != null)
            listener(qty);
    }

    public override void SetQty(int qty)
    {
        base.SetQty(qty);
        if (listener != null)
            listener(qty);
    }

    public override int GetQty(int q)
    {
        int value = base.GetQty(q);
        if (listener != null)
            listener(qty);

        return value;
    }

    public override bool Get(int qty)
    {
        bool value = base.Get(qty);
        if (listener != null)
            listener(qty);
        return value;
    }

    public override bool Get()
    {
        bool value = base.Get();
        if (listener != null)
            listener(qty);
        return value;
    }
    */
}
