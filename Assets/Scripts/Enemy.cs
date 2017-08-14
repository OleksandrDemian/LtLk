using System;
using UnityEngine;

public class Enemy : MCharacterController
{
    [SerializeField]
    private bool isAggressive = true;

    protected override void Start()
    {
        base.Start();
    }

    public override void CharacterStateListener(CharacterEvents cEvent)
    {
        switch (cEvent)
        {
            case CharacterEvents.TURN_END:
                CheckPlayer();
                break;
        }
    }

    private void CheckPlayer()
    {
        if (!isAggressive)
            return;

        Character player = Player.Instance.GetCharacter();
        if (player.IsStealthy())
            return;

        if (character.Distance(player) < 2)
        {
            BattleManager battle = new BattleManager(character, player);
            battle.StartBattle();
        }
    }

    public override void OnBattleEnd(bool won, Character enemy)
    {
        
    }

    #region AttributesValueHandlers

    public override void OnDamageValueChange(int value, int oldValue)
    {

    }

    public override void OnHealthValueChange(int value, int oldValue)
    {

    }

    public override void OnStaminaValueChange(int value, int oldValue)
    {

    }

    #endregion
}
