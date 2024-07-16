using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] public string variableName;
    [SerializeField] public float changeVariableFloat;

    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    public void UpdateVariable()
    {
        //player movement
        if (variableName == "jumpPower")
            playerMovement.ChangeAttribute(variableName, changeVariableFloat);
        else if (variableName == "moveSpeed")
            playerMovement.ChangeAttribute(variableName, changeVariableFloat);

        //player attack
        else if (variableName == "damage")//must be int
            playerAttack.ChangeAttribute(variableName, changeVariableFloat);
        else if (variableName == "attackRange")
            playerAttack.ChangeAttribute(variableName, changeVariableFloat);

        else{
            Debug.Log("no variableName in upgrade system.");
        }
    }
}
