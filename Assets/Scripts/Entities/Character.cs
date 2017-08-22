using UnityEngine;

public enum CharacterEvents
{
    HEALTH_CHANGE,
    STAMINA_CHANGE,
    DAMAGE_CHANGE,
    DEAD,
    TURN_END,
    BATTLE_WON,
    BATTLE_START,
    TURN_START
}

public class Character : Entity
{
    protected Attribute health;
    protected Attribute damage;
    protected Attribute stamina;

    protected bool didSomeAction = false;

    private bool isPlayer = false;

    protected MCharacterController controller;

    public override void OnGameStart()
    {
        base.OnGameStart();
        MCharacterController c = GetComponent<MCharacterController>();
        c.Initialize(this);
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
            if(controller.InteractWith(node.GetEntity()))
                didSomeAction = true;
        }
        else
        {
            MoveToNode(node);
            this.x = x;
            this.y = y;
            UnityEngine.Debug.Log(name + " Position: " + X + " " + Y);
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
        return;
        /*
        if (actor is Character)
        {
            BattleManager battle = new BattleManager(this, actor as Character);
            battle.StartBattle();
        }
        */
    }

    public virtual bool IsStealthy()
    {
        return false;
    }

    public void SetController(MCharacterController controller)
    {
        this.controller = controller;
    }

    public MCharacterController GetController()
    {
        return controller;
    }

    public virtual void ApplyDamage(int amount, Character actor)
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

    #region TurnManagment

    public virtual void OnTurnStart()
    {

    }

    public override void Turn()
    {        
        if (!controller.StartTurn())
        {
            EndTurn(false);
            return;
        }
        
        didSomeAction = false;
        OnTurnStart();
        controller.TurnUpdate();
    }

    public virtual void OnTurnEnd()
    {

    }

    public void EndTurn(bool onTurnEnd)
    {
        if(onTurnEnd)
            OnTurnEnd();
        GameManager.Instance.OnEntityTurnEnd(this);
    }

    #endregion

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

    public void SetIsPlayer(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    public void Attack(Character target)
    {
        int damage = CalculateDamage();
        target.ApplyDamage(damage, this);
        OnAttackDone(target);

        Debug.Log(name + " >> " + target.name + ": " + damage);
        Vector3 animationDirection = (target.transform.position - transform.position)/3;
        controller.AnimateAttack(animationDirection);
        didSomeAction = true;
    }

    protected virtual void OnCharacterMoved()
    {
        stamina.Value--;
    }

    protected virtual void OnAttackDone(Character victim)
    {
        stamina.Value--;
    }
}
