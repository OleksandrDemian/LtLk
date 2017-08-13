using UnityEngine;

public enum PlayerEvents
{
    DEAD,
    ENDTURN
}

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get;
        private set;
    }

    private Character character;
    private bool movementEnabled = true;

    private PlayerHUD hud;

    private int gold = 10;
    private Attribute healthPotion;
    private Attribute staminaPotion;

    [Header("Character attributes")]
    [SerializeField]
    private int defaultHealth = 10;
    [SerializeField]
    private int defaultDamage = 10;
    [SerializeField]
    private int defaultStamina = 10;
    [SerializeField]
    private string pName = "DefaultName";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hud = PlayerHUD.Instance;
        character = GetComponent<Character>();
        character.onCharacterStateChange = CharacterStateListener;

        XmlReader.LoadPlayer(this);

        character.SetHealth(defaultHealth);
        character.SetDamage(defaultDamage);
        character.SetStamina(defaultStamina);
        character.name = pName;

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
            character.GetStamina().Value--;
            GameManager.Instance.PlayerEvent(PlayerEvents.ENDTURN);
        }
        else
        {
            InformationWindow.ShowInformation("Info", "You cannot move there!", false);
        }
    }

    private void CharacterStateListener(CharacterEvents cEvent)
    {
        switch (cEvent)
        {
            case CharacterEvents.DEAD:
                GameManager.Instance.PlayerEvent(PlayerEvents.DEAD);
                break;
            case CharacterEvents.STAMINA_CHANGE:
                //InformationWindow.ShowInformation("Test", "Player's stamina value was changed: " + character.GetStamina().Value);
                hud.SetStamina(character.GetStamina().Value);
                break;
            case CharacterEvents.HEALTH_CHANGE:
                hud.SetHealth(character.GetHealth().Value);
                break;
        }
    }

    public void EnableMovement(bool action)
    {
        movementEnabled = action;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public void Sleep()
    {
        character.GetStamina().Value += 4;
        InformationWindow.ShowInformation("Sleep", "You have slept for a while and now you feel better!", false);
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

    #endregion
}