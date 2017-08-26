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

    public override void Initialize(Character character)
    {
        base.Initialize(character);
        Instance = this;
        character.SetIsPlayer(true);
        InitializeInventory();
        InitializeHUD();
        CameraController.Instance.SetTarget(transform);
    }

    private void InitializeHUD()
    {
        hud = PlayerHUD.Instance;

        hud.SetName(name);
        hud.SetGold(character.GetInventory().GetItem("Gold").GetQty());
        hud.SetStamina(character.GetStamina().Value);
        hud.SetHealth(character.GetHealth().Value);
    }

    private void InitializeInventory()
    {
        EItem gold = new EItem("Gold", 20);
        EItem healthPotion = new EItem("Health potion", 2);
        EItem staminaPotion = new EItem("Stamina potion", 2);

        Inventory inventory = character.GetInventory();
        inventory.AddItem(gold, true);
        inventory.AddItem(healthPotion, true);
        inventory.AddItem(staminaPotion, true);

        gold.SetListener(OnGoldQtyChange);
        healthPotion.SetListener(OnHealthPotionQtyChange);
        staminaPotion.SetListener(OnStaminaPotionQtyChange);
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
            EndTurn();
        }
        else
        {
            InformationWindow.ShowInformation("Info", "You cannot move there!", false, "movefail");
        }
    }

    public override void OnCharacterDead()
    {
        base.OnCharacterDead();
        GameManager.Instance.PlayerEvent(PlayerEvents.DEAD);
    }

    public override bool StartTurn()
    {
        if (character.GetStamina().Value > 0)
        {
            EnableMovement(true);
            CameraController.Instance.SetTarget(transform);
            //InformationWindow.ShowInformation("Turn", "It is your turn now!", false, "playersturn");
            Toast.ShowToast("It is your turn now!", .5f);
            return true;
        }
        return false;
    }

    public override void TurnUpdate()
    {
        if (!StartTurn())
            EndTurn();
    }

    public void EnableMovement(bool action)
    {
        movementEnabled = action;
    }

    public void Sleep()
    {
        character.RestoreStamina(4);
        character.RestoreHealth(1);
        InformationWindow.ShowInformation("Sleep", "You have slept for a while and now you feel better!");
    }

    protected override void EndTurn()
    {
        if (!character.DidSomeAction)
        {
            Sleep();
        }

        EnableMovement(false);

        if (!isAnimated)
            ControllerEndTurn();
    }

    public void Rest()
    {
        if (movementEnabled)
            EndTurn();
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

    #region PotionsManagment

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

        Item hp = character.GetHealthPotions();
        if (hp.Get())
        {
            character.GetHealth().ResetValue();
        }
    }

    public void DrinkStaminaPotion()
    {
        if (!movementEnabled)
            return;

        Item sp = character.GetStaminaPotions();
        if (sp.Get())
        {
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