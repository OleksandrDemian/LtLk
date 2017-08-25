using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private Text pName;
    [SerializeField]
    private Text pHealth;
    [SerializeField]
    private Text pGold;
    [SerializeField]
    private Text pStamina;
    [SerializeField]
    private Text pHealthPotions;
    [SerializeField]
    private Text pStaminaPotions;

    public static PlayerHUD Instance
    {
        get;
        private set;
    }

	private void Awake ()
    {
        Instance = this;
	}

    public void SetHealth(int value)
    {
        pHealth.text = "Health: " + value;
    }

    public void SetName(string value)
    {
        pName.text = "Name: " + value;
    }

    public void SetStamina(int value)
    {
        pStamina.text = "Stamina: " + value;
    }

    public void SetGold(int value)
    {
        pGold.text = "Gold: " + value;
    }

    public void SetHealthPotions(int qty)
    {
        pHealthPotions.text = "Health potions: " + qty;
    }

    public void SetStaminaPotion(int qty)
    {
        pStaminaPotions.text = "Stamina potions: " + qty;
    }

    public void DrinkHealthPotion()
    {
        Player.Instance.DrinkHealthPotion();
    }

    public void DrinkStaminaPotion()
    {
        Player.Instance.DrinkStaminaPotion();
    }

    public void EndTurn()
    {
        Player.Instance.Rest();
    }
}
