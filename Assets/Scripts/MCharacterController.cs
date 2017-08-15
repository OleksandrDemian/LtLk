using UnityEngine;

public abstract class MCharacterController : MonoBehaviour
{
    protected Character character;

    [SerializeField]
    protected bool initializeCharacter = false;

    [Header("Character attributes")]
    [SerializeField]
    protected int defaultHealth = 10;
    [SerializeField]
    protected int defaultDamage = 10;
    [SerializeField]
    protected int defaultStamina = 10;
    [SerializeField]
    protected string cName = "DefaultName";

    protected virtual void Start ()
    {
        character = GetComponent<Character>();
        character.SetController(this);
        if (initializeCharacter)
        {
            character.SetHealth(defaultHealth);
            character.SetDamage(defaultDamage);
            character.SetStamina(defaultStamina);
            character.name = cName;
        }
    }

    public Character GetCharacter()
    {
        return character;
    }

    public abstract void CharacterStateListener(CharacterEvents cEvent);
    public abstract void OnHealthValueChange(int value, int oldValue);
    public abstract void OnDamageValueChange(int value, int oldValue);
    public abstract void OnStaminaValueChange(int value, int oldValue);
    public abstract void OnBattleEnd(bool won, Character enemy);
    public abstract Item GetGold();
}
