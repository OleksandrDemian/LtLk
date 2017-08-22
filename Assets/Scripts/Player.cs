using UnityEngine;
using UnityEngine.EventSystems;

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
    private bool movementEnabled = false;
    private PlayerHUD hud;
    private Inventory inventory;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        character.SetIsPlayer(true);
    }

    private void Start()
    {
        hud = PlayerHUD.Instance;

        EItem gold = new EItem("Gold", 20);
        EItem healthPotion = new EItem("Health potion", 2);
        EItem staminaPotion = new EItem("Stamina potion", 2);

        inventory = new Inventory();
        inventory.AddItem(gold);
        inventory.AddItem(healthPotion);
        inventory.AddItem(staminaPotion);

        gold.SetListener(OnGoldQtyChange);
        healthPotion.SetListener(OnHealthPotionQtyChange);
        staminaPotion.SetListener(OnStaminaPotionQtyChange);

        hud.SetName(name);
        hud.SetGold(gold.GetQty());
        hud.SetStamina(character.GetStamina().Value);
        hud.SetHealth(character.GetHealth().Value);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckCell();
        }

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

    private void CheckCell()
    {
        //if pointer is on some GUI element: do not do raycast
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 targetPoint = hit.collider.transform.position;

            Debug.Log(hit.collider.name + " " + targetPoint);

            Entity e = MapManager.Instance.GetNode((int)targetPoint.x, (int)targetPoint.z).GetEntity();
            if(e != null)
                e.ShowInfo();
        }
    }

    private void MoveCharacter(int x, int y)
    {
        Attribute stamina = character.GetStamina();
        if (stamina.Value < 1)
        {
            InformationWindow.ShowInformation("To tired", "You are to tired to move", false, "tiered");
            return;
        }
        bool moved = character.MoveDirection(x, y);
        if (moved)
        {
            //GameManager.Instance.PlayerEvent(PlayerEvents.ENDTURN);
            EndTurn();
        }
        else
        {
            InformationWindow.ShowInformation("Info", "You cannot move there!", false, "movefail");
        }
    }

    public override void CharacterStateListener(CharacterEvents cEvent)
    {
        switch (cEvent)
        {
            case CharacterEvents.DEAD:
                GameManager.Instance.PlayerEvent(PlayerEvents.DEAD);
                break;
            case CharacterEvents.TURN_END:
                EnableMovement(false);
                EndTurn();
                break;
        }
    }

    public override bool StartTurn()
    {
        EnableMovement(true);
        CameraController.Instance.SetTarget(transform);
        //InformationWindow.ShowInformation("Turn", "It is your turn now!", false, "playersturn");
        Toast.ShowToast("It is your turn now!", .5f);
        return true;
    }

    public override void TurnUpdate()
    {
        
    }

    public void EnableMovement(bool action)
    {
        movementEnabled = action;
    }

    public void Sleep()
    {
        if (!movementEnabled)
            return;

        character.GetStamina().Value += 4;
        character.GetHealth().Value++;
        InformationWindow.ShowInformation("Sleep", "You have slept for a while and now you feel better!");
    }

    public void EndTurn()
    {
        if (!movementEnabled)
            return;

        if (!character.DidSomeAction)
        {
            Sleep();
        }

        EnableMovement(false);
        character.EndTurn(true);
    }

    public override bool InteractWith(Entity target)
    {
        if (target is Character)
        {
            Character c = target as Character;
            if (!c.IsPlayer)
            {
                character.Attack(c);
            }
        }
        else
        {
            target.Interact(character);
        }
        return true;
    }

    public Inventory Inventory
    {
        get
        {
            return inventory;
        }
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
        Item gold = inventory.GetItem("Gold");
        gold.SetQty(qty);
    }

    public void AddGold(int qty)
    {
        Item gold = inventory.GetItem("Gold");
        gold.AddQty(qty);
    }

    public override Item GetGold()
    {
        Item gold = inventory.GetItem("Gold");
        return gold;
    }
    #endregion

    #region PotionsManagment

    public void AddHealthPotion(int qty)
    {
        inventory.AddItem(new Item("Health potion", qty));
    }

    public void AddStaminaPotion(int qty)
    {
        inventory.AddItem(new Item("Stamina potion", qty));
    }

    public void OnStaminaPotionQtyChange(int value)
    {
        hud.SetStaminaPotion(value);
    }

    public void OnHealthPotionQtyChange(int value)
    {
        hud.SetHealthPotions(value);
    }

    public void OnGoldQtyChange(int value)
    {
        hud.SetGold(value);
    }

    public void DrinkHealthPotion()
    {
        if (!movementEnabled)
            return;
        Item hp = inventory.GetItem("Health potion");
        if (hp.GetQty() > 0)
        {
            hp.Decrease();
            character.GetHealth().ResetValue();
        }
    }

    public void DrinkStaminaPotion()
    {
        if (!movementEnabled)
            return;

        Item sp = inventory.GetItem("Stamina potion");
        if (sp.GetQty() > 0)
        {
            sp.Decrease();
            character.GetStamina().ResetValue();
        }
    }

    #endregion

    public void Training(float mod)
    {
        int incHealth = Random.Range(0, 4);
        int incDamage = Random.Range(0, 2);
        int incStamina = Random.Range(0, 2);
        

        string msg = "";

        if (incHealth > 0)
        {
            incHealth = (int)(incHealth * mod);
            msg += "Your health rised up by " + incHealth + " points!\n";
            character.GetHealth().IncreaseDefaultValue(incHealth);
        }
        if (incDamage > 0)
        {
            incDamage = (int)(incDamage * mod);
            msg += "Your damage rised up by " + incDamage + " points!\n";
            character.GetDamage().IncreaseDefaultValue(incDamage);
        }
        if (incStamina > 0)
        {
            incStamina = (int)(incStamina * mod);
            msg += "Your stamina rised up by " + incStamina + " points!";
            character.GetStamina().IncreaseDefaultValue(incStamina);
        }

        InformationWindow.ShowInformation("Train result", msg);
    }
}