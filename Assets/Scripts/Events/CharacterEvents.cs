using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Enums;
public class CharacterEvents
{
    public delegate void DamageAction(GameObject character, int damage, Enums.DamageType damageType);

    public static event DamageAction characterDamaged;


    public static void TriggerCharacterDamaged(GameObject character, int damage, Enums.DamageType damageType)
    {
        characterDamaged?.Invoke(character, damage, damageType);
    }


    public static UnityAction<GameObject, int> characterHealed;
}