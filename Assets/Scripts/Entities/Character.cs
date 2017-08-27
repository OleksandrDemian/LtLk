using UnityEngine;

public class Character : Entity
{
    protected Attribute health;
    protected Attribute damage;
    protected Attribute stamina;
    protected bool didSomeAction = false;
    private bool isPlayer = false;
    protected Inventory inventory;
    protected MCharacterController controller;

    public override void OnGameStart()
    {
        base.OnGameStart();
        inventory = new Inventory();
        controller = GetComponent<MCharacterController>();
        controller.Initialize(this);
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
            //node.GetEntity().Interact(this);
            if (controller.InteractWith(node.GetEntity()))
                didSomeAction = true;
            else
                return false;
        }
        else
        {
            MoveToNode(node);
            this.x = x;
            this.y = y;
            //transform.position = new Vector3(x, transform.position.y, y);
            didSomeAction = true;
            controller.AnimateMovement(new Vector3(x, transform.position.y, y));
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
        if (value < 1)
            Death();
        else
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

    #region RestoreAttributes
    public virtual void RestoreHealth(int amount)
    {
        health.Value += amount;
    }

    public virtual void RestoreStamina(int amount)
    {
        stamina.Value += amount;
    }
    #endregion

    public bool DidSomeAction
    {
        set
        {
            didSomeAction = value;
        }
        get
        {
            return didSomeAction;
        }
    }

    public void Death()
    {
        bool isDead = OnCharacterDeath();
        if (!isDead)
            return;

        controller.OnCharacterDead();
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
        return;
    }

    public virtual bool IsStealthy()
    {
        return false;
    }

    public MCharacterController GetController()
    {
        return controller;
    }

    /// <summary>
    /// Decrease health value by amount
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="actor"></param>
    public virtual void ApplyDamage(int amount, Character actor)
    {
        HistoryTracer.Instance.AddToHistory(actor.name + ">>" + name + ": " + amount);
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

    #region TurnManagment

    public virtual void OnTurnStart()
    {

    }

    public override void Turn()
    {
        didSomeAction = false;
        controller.TurnUpdate();
    }

    public virtual void OnTurnEnd()
    {

    }
    #endregion

    public virtual void GenerateLoot(Player player)
    {
        float mod = (GetPower() / 50f);
        int gold = (int)(Random.Range(10, 20) * mod);
        Item goldI = player.GetCharacter().GetInventory().GetItem("Gold");
        goldI.AddQty(gold);
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

    public void SetIsPlayer(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }

    public void Attack(Character target)
    {
        int damage = CalculateDamage();
        target.ApplyDamage(damage, this);

        if (!IsAlive())
        {
            return;
        }

        OnAttackDone(target);

        Debug.Log(name + " >> " + target.name + ": " + damage);
        Vector3 animationDirection = (target.transform.position - transform.position)/3;
        controller.AnimateAttack(animationDirection);
    }

    protected virtual void OnCharacterMoved()
    {
        stamina.Value--;
    }

    protected virtual void OnAttackDone(Character victim)
    {
        stamina.Value--;
    }

    protected virtual bool OnCharacterDeath()
    {
        return true;
    }

    #region Inventory
    public Inventory GetInventory()
    {
        return inventory;
    }

    public Item GetGold()
    {
        return inventory.GetItem("Gold");
    }

    public Item GetHealthPotions()
    {
        return inventory.GetItem("Health potion");
    }

    public Item GetStaminaPotions()
    {
        return inventory.GetItem("Stamina potion");
    }
    #endregion
}
