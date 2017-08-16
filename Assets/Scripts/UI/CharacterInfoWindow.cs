using UnityEngine.UI;
using UnityEngine;
using System;

public class CharacterInfoWindow : MonoBehaviour, IPoolable
{
    [SerializeField]
    private Text tName;
    [SerializeField]
    private Text health;
    [SerializeField]
    private Text damage;
    [SerializeField]
    private Text stamina;
    [SerializeField]
    private Text power;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public static void Show(Character character)
    {
        CharacterInfoWindow window = ObjectPool.Get<CharacterInfoWindow>();
        window.SetName(character.name);

        Attribute health = character.GetHealth();
        Attribute stamina = character.GetStamina();
        Attribute damage = character.GetDamage();

        window.SetHealth(health.Value, health.GetMax());
        window.SetDamage(damage.Value);
        window.SetStamina(stamina.Value, stamina.GetMax());
        window.SetPower(character.GetPower());
    }

    public void Close()
    {
        ObjectPool.Add(this);
    }

    public void SetPower(int value)
    {
        power.text = "Power: " + value;
    }

    public void SetDamage(int damage)
    {
        this.damage.text = "Damage: " + damage;
    }

    public void SetName(string name)
    {
        tName.text = name;
    }

    public void SetHealth(int value, int max)
    {
        health.text = "Health: " + value + "/" + max;
    }

    public void SetStamina(int value, int max)
    {
        stamina.text = "Stamina: " + value + "/" + max;
    }
}
