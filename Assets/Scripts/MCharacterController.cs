using System.Collections;
using UnityEngine;

public abstract class MCharacterController : MonoBehaviour
{
    protected Character character;
    protected int animationSpeed = 5;

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

    public virtual void Initialize (Character character)
    {
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
        this.character = character;
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

    protected IEnumerator AttackAnimation(Vector3 dir)
    {
        Vector3 endPoint = transform.position + dir;
        while (transform.position != endPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * animationSpeed);
            yield return null;
        }
        transform.position = new Vector3(character.X, transform.position.y, character.Y);
        Debug.Log("End attack animation!");
    }

    protected IEnumerator MoveAnimation(Vector3 endPoint)
    {
        while (transform.position != endPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * animationSpeed);
            yield return null;
        }
        Debug.Log("End move animation!");
    }

    public void AnimateMovement(Vector3 target)
    {
        StartCoroutine(MoveAnimation(target));
    }

    public void AnimateAttack(Vector3 dir)
    {
        StartCoroutine(AttackAnimation(dir));
    }
}
