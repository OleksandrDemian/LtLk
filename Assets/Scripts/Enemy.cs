using UnityEngine;

[RequireComponent(typeof(Character))]
public class Enemy : MonoBehaviour
{
    [Header("Character attributes")]
    [SerializeField]
    private int defaultHealth = 10;
    [SerializeField]
    private int defaultDamage = 10;
    [SerializeField]
    private int defaultStamina = 10;
    [SerializeField]
    private string eName = "DefaultEnemyName";

    private Character character;

    private void Start ()
    {
        character = GetComponent<Character>();
        character.SetDamage(defaultDamage);
        character.SetHealth(defaultHealth);
        character.SetStamina(defaultStamina);
        character.onCharacterStateChange = CharacterStateListener;
        character.name = eName;
	}

    private void CharacterStateListener(CharacterEvents cEvent)
    {
        switch (cEvent)
        {
            case CharacterEvents.DEAD:
                InformationWindow.ShowInformation("Death", name + " is dead!");
                break;
        }
    }
}
