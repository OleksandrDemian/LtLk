using System.Collections;
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
        else
        {
            character.InitializeAttributes();
        }
    }

    public Character GetCharacter()
    {
        return character;
    }

    public abstract void CharacterStateListener(CharacterEvents cEvent);
    public abstract bool StartTurn();
    public abstract void TurnUpdate();
    public abstract void OnHealthValueChange(int value, int oldValue);
    public abstract void OnDamageValueChange(int value, int oldValue);
    public abstract void OnStaminaValueChange(int value, int oldValue);
    public abstract Item GetGold();
    public abstract bool InteractWith(Entity target);

    protected IEnumerator AttackAnimation()
    {
        yield return null;
    }

    protected IEnumerator MoveAnimation(Vector3 endPoint)
    {
        while (transform.position != endPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * 5);
            yield return null;
        }
        Debug.Log("End move animation!");
    }

    public void AnimateMovement(Vector3 target)
    {
        StartCoroutine(MoveAnimation(target));
    }
}
