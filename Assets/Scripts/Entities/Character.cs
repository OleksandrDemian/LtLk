using UnityEngine;

public enum CharacterEvents
{
    HEALTH_CHANGE,
    STAMINA_CHANGE,
    DAMAGE_CHANGE,
    DEAD,
    TURN_END,
    BATTLE_WON,
    BATTLE_START
}

public class Character : Entity
{
    protected Attribute health;
    protected Attribute damage;
    protected Attribute stamina;

    protected MCharacterController controller;

    protected override void Start()
    {
        base.Start();
        //InitializeAttributes();
    }

    public virtual void InitializeAttributes()
    {
        name = "Default";
        SetDamage(10);
        SetHealth(10);
        SetStamina(10);
    }

    public bool Move(int x, int y)
    {
        Node node = MapManager.Instance.GetNode(x, y);
        
        if (node == null)
            return false;
        if (!node.IsWalkable)
            return false;

        if (node.HasEntity())
        {
            node.GetEntity().Interact(this);
        }
        else
        {
            MoveToNode(node);
            transform.position = new Vector3(x, transform.position.y, y);
        }
        return true;
    }

    public bool MoveDirection(int x, int y)
    {
        return Move(x + X, y + Y);
    }

    #region Attributes Get/Set
    public Attribute GetHealth()
    {
        return health;
    }

    public void SetHealth(int value)
    {
        health = new Attribute(value);
        health.onValueChange = OnHealthValueChange;
    }

    public Attribute GetStamina()
    {
        return stamina;
    }

    public void SetStamina(int value)
    {
        stamina = new Attribute(value);
        stamina.onValueChange = OnStaminaValueChange;
    }

    public Attribute GetDamage()
    {
        return damage;
    }

    public void SetDamage(int value)
    {
        damage = new Attribute(value);
        damage.onValueChange = OnDamageValueChange;
    }
    #endregion

    #region AttributesListeners

    protected virtual void OnHealthValueChange(int value, int oldValue)
    {
        controller.OnHealthValueChange(value, oldValue);
    }

    protected virtual void OnDamageValueChange(int value, int oldValue)
    {
        controller.OnDamageValueChange(value, oldValue);
    }

    protected virtual void OnStaminaValueChange(int value, int oldValue)
    {
        controller.OnStaminaValueChange(value, oldValue);
    }

    #endregion

    public void Death()
    {
        controller.CharacterStateListener(CharacterEvents.DEAD);
        gameObject.SetActive(false);
    }

    protected void MoveToNode(Node node)
    {
        currentNode.ReleaseEntity();
        node.SetEntity(this);
        currentNode = node;
        OnCharacterMoved();
    }

    public override void Interact(Entity actor)
    {
        if (actor is Character)
        {
            BattleManager battle = new BattleManager(this, actor as Character);
            battle.StartBattle();
        }
    }

    public virtual bool IsStealthy()
    {
        return false;
    }

    public virtual void OnBattleStart(Character enemy)
    {
        controller.CharacterStateListener(CharacterEvents.BATTLE_START);
    }

    public virtual void OnBattleTurn(int turnIndex, Character enemy)
    {
        stamina.Value--;
    }

    public virtual void OnBattleEnd(Character enemy)
    {
        if (IsAlive())
        {
            controller.OnBattleEnd(true, enemy);
        }
        else
        {
            controller.OnBattleEnd(false, enemy);
            Death();
        }
    }

    protected virtual void OnCharacterMoved()
    {
        stamina.Value--;
    }

    public void SetController(MCharacterController controller)
    {
        this.controller = controller;
    }

    public MCharacterController GetController()
    {
        return controller;
    }

    public virtual void ApplyDamage(int amount)
    {
        health.Value -= amount;
    }

    public virtual int CalculateDamage()
    {
        int finalDamage = (int)(damage.Value * GetFatigueModifier());
        return finalDamage > 0 ? finalDamage : 1;
    }

    private float GetFatigueModifier()
    {
        return stamina.Value / (float)stamina.GetMax();
    }

    public bool IsAlive()
    {
        return health.Value > 0 ? true : false;
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        controller.CharacterStateListener(CharacterEvents.TURN_END);
    }

    public virtual void GenerateLoot(Player player)
    {
        float mod = (GetPower() / 50f);
        int gold = (int)(Random.Range(10, 20) * mod);
        player.AddGold(gold);
        InformationWindow.ShowInformation("Gold!", "You have found " + gold + " gold on " + name);
    }

    public int GetPower()
    {
        return health.GetMax() + damage.GetMax() + stamina.GetMax();
    }

    public virtual string GetDescription()
    {
        return "This is a bas character class";
    }

    public override void ShowInfo()
    {
        CharacterInfoWindow.Show(this);
    }
}
