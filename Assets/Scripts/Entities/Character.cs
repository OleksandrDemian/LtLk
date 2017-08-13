using UnityEngine;

public delegate void CharacterEvent(CharacterEvents cEvent);

public enum CharacterEvents
{
    HEALTH_CHANGE,
    STAMINA_CHANGE,
    DAMAGE_CHANGE,
    DEAD,
    TURN_END
}

public class Character : Entity
{
    protected Attribute health;
    protected Attribute damage;
    protected Attribute stamina;

    public CharacterEvent onCharacterStateChange;

    protected override void Start()
    {
        base.Start();
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

    private void OnHealthValueChange(int value, int oldValue)
    {
        if (onCharacterStateChange != null)
            onCharacterStateChange(CharacterEvents.HEALTH_CHANGE);
    }

    private void OnDamageValueChange(int value, int oldValue)
    {
        if (onCharacterStateChange != null)
            onCharacterStateChange(CharacterEvents.DAMAGE_CHANGE);
    }

    private void OnStaminaValueChange(int value, int oldValue)
    {
        if (onCharacterStateChange != null)
            onCharacterStateChange(CharacterEvents.STAMINA_CHANGE);
    }

    #endregion

    public void Death()
    {
        if (onCharacterStateChange != null)
            onCharacterStateChange(CharacterEvents.DEAD);
        gameObject.SetActive(false);
    }

    protected void MoveToNode(Node node)
    {
        currentNode.ReleaseEntity();
        node.SetEntity(this);
        currentNode = node;
    }

    public override void Interact(Entity actor)
    {
        if (actor.GetType() == typeof(Character))
        {
            BattleManager battle = new BattleManager(this, actor as Character);
            battle.StartBattle();
        }
    }

    protected override void Update()
    {
        
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        if (onCharacterStateChange != null)
            onCharacterStateChange(CharacterEvents.TURN_END);
    }
}
