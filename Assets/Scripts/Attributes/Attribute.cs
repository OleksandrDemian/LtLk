using System.Collections.Generic;

public delegate void OnValueChange(int value, int oldValue);

public class Attribute
{
    public OnValueChange onValueChange;
    private int value;
    private int defaultValue;
    private int maxValue;
    private List<AttributeModifier> modifiers = new List<AttributeModifier>();

    public Attribute(int value) {
        this.value = value;
        maxValue = value;
        defaultValue = value;
    }

    public int Value {
        get
        {
            return value;
        }
        set
        {
            int old = this.value;
            this.value = UnityEngine.Mathf.Clamp(value, 0, maxValue);

            if (onValueChange != null)
                onValueChange(Value, old);
        }
    }

    public void ResetValue()
    {
        Value = maxValue;
    }

    public void ResetDefaultValue()
    {
        maxValue = defaultValue;
        value = maxValue;
        ClearModifiers();
    }

    public void AddModifier(AttributeModifier modifier)
    {
        int old = Value;
        modifiers.Add(modifier);
        CalculateValue();

        if (onValueChange != null)
            onValueChange(Value, old);
    }

    public void RemoveModifier(AttributeModifier modifier)
    {
        modifiers.Remove(modifier);
        CalculateValue();
    }

    public void ClearModifiers()
    {
        modifiers.Clear();
        CalculateValue();
    }

    public override string ToString()
    {
        return " Value: " + Value + " MaxValue: " + maxValue + " DefaultValue: " + defaultValue;
    }

    public void SetMax(int value)
    {
        maxValue = value;
        defaultValue = value;
        Value = value;
    }

    public void IncreaseDefaultValue(int amount)
    {
        int target = defaultValue + amount;
        SetMax(target);
    }

    private void CalculateValue()
    {
        int currentValuePercent = (value * 100) / maxValue;
        //Debug.Log("Percents: Value: " + value + " max: " + maxValue + " percents: " + currentValuePercent);

        int finalValue = defaultValue;
        int add = 0;
        int mult = 1;

        foreach (AttributeModifier modifier in modifiers)
        {
            //modifier.Apply(ref finalValue);
            switch (modifier.Type)
            {
                case ModifierType.ADD:
                    modifier.Apply(ref add);
                    break;
                case ModifierType.MULTIPLY:
                    modifier.Apply(ref mult);
                    break;
            }
        }

        finalValue += add;
        //if (mult != 0)
        finalValue *= mult;
        

        maxValue = finalValue;
        Value = (currentValuePercent * maxValue) / 100;
    }
}