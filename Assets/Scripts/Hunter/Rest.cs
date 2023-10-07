using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : State
{
    public float restCooldown=5f;
    public float cooldown;
    Hunter _hunter;

    public Rest(Hunter p)
    {
        _hunter = p;
    }

    public override void OnEnter()
    {
        cooldown = 0;
        Debug.Log("Entre a Rest");

    }

    public override void OnUpdate()
    {
        //;//Comentario
        if (cooldown <= restCooldown)
        {
            cooldown += Time.deltaTime;
        }
        else if (cooldown > restCooldown)
        {
            cooldown = 0;
            Hunter.instance.energy = 10;
        }
        if (Hunter.instance.energy == 10)
        {
            fsm.ChangeState(EnemyActions.Chase);
            Debug.Log("como la energia es igual a 10, deberia irse a chase");
        }
        else
        {
            Debug.Log("perrito");
        }
        Debug.Log("Estoy en Rest");
    }

    public override void OnExit()
    {
        Debug.Log("Sali de Rest");
    }

   
}