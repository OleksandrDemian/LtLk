using System;
using UnityEngine;

public enum PlayerEvents
{
    DEAD,
    ENDTURN
}

public class Player : MCharacterController
{
    public static Player Instance
    {
        get;
        private set;
    }

    private bool movementEnabled = true;

    private PlayerHUD hud;

    private int gold = 10;
    private Attribute healthPotion;
    private Attribute staminaPotion;    

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        hud = PlayerHUD.Instance;

        healthPotion = new Attribute(4);
        healthPotion.Value = 2;
        staminaPotion = new Attribute(4);
        staminaPotion.Value = 2;
        healthPotion.onValueChange = OnHealthPotionQtyChange;
        staminaPotion.onValueChange = OnStaminaPotionQtyChange;

        hud.SetName(name);
        hud.SetGold(gold);
        hud.SetStamina(defaultStamina);
        hud.SetHealth(defaultHealth);
    }

    private void Update()
    {
        if (!movementEnabled)
            return;

        if (Input.GetKeyDown(KeyCode.A))
            MoveCharacter(-1, 0);
        if (Input.GetKeyDown(KeyCode.D))
            MoveCharacter(1, 0);
        if (Input.GetKeyDown(KeyCode.W))
            MoveCharacter(0, 1);
        if (Input.GetKeyDown(KeyCode.S))
            MoveCharacter(0, -1);

        if (Input.GetKeyDown(KeyCode.H))
        {
            DrinkHealthPotion();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            DrinkStaminaPotion();
        }
    }

    private void MoveCharacter(int x, int y)
    {
        Attribute stamina = character.GetStamina();
        if (stamina.Value < 1)
        {
            InformationWindow.ShowInformation("To tired", "You are to tired to move", false);
            return;
        }
        bool moved = character.MoveDirection(x, y);
        if (moved)
        {
            stamina.Value--;
            GameManager.Instance.PlayerEvent(PlayerEvents.ENDTURN);
        }
        else
        {
            InformationWindow.ShowInformation("Info", "You cannot move there!", false);
        }
    }

    public override void CharacterStateListener(CharacterEvents cEvent)
    {
        switch (cEvent)
        {
            case CharacterEvents.DEAD:
                GameManager.Instance.PlayerEvent(PlayerEvents.DEAD);
                break;
            case CharacterEvents.BATTLE_WON:
                break;
        }
    }

    public void EnableMovement(bool action)
    {
        movementEnabled = action;
    }

    public void Sleep()
    {
        character.GetStamina().Value += 4;
        InformationWindow.ShowInformation("Sleep", "You have slept for a while and now you feel better!", false);
    }

    public override void OnBattleEnd(bool won, Character enemy)
    {
        if(won)
            enemy.GenerateLoot(this);
    }

    #region AttributesValueHandlers

    public override void OnDamageValueChange(int value, int oldValue)
    {
        
    }

    public override void OnHealthValueChange(int value, int oldValue)
    {
        hud.SetHealth(value);
    }

    public override void OnStaminaValueChange(int value, int oldValue)
    {
        hud.SetStamina(value);
    }

    #endregion

    #region GoldManagment
    public void SetGold(int qty)
    {
        gold = qty;
        hud.SetGold(gold);
    }

    public void AddGold(int qty)
    {
        gold += qty;
        hud.SetGold(gold);
    }

    public int GetGold()
    {
        return gold;
    }
    #endregion

    #region PotionsManagment

    public void AddHealthPotion(int qty)
    {
        healthPotion.Value += qty;
    }

    public void AddStaminaPotion(int qty)
    {
        staminaPotion.Value += qty;
    }

    public void OnStaminaPotionQtyChange(int value, int oldValue)
    {
        hud.SetStaminaPotion(value);
    }

    public void OnHealthPotionQtyChange(int value, int oldValue)
    {
        hud.SetHealthPotions(value);
    }

    public void DrinkHealthPotion()
    {
        if (healthPotion.Value > 0)
        {
            healthPotion.Value--;
            character.GetHealth().ResetValue();
        }
    }

    public void DrinkStaminaPotion()
    {
        if (staminaPotion.Value > 0)
        {
            staminaPotion.Value--;
            character.GetStamina().ResetValue();
        }
    }

    #endregion
}