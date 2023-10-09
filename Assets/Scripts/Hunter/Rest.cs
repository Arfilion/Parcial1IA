using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : State
{
    public float restCooldown = 5f;
    public float cooldown;
    Hunter _hunter;
    Renderer _rend;
    public Transform _target;

    public Rest(Hunter p)
    {
        _hunter = p;
    }

    public override void OnEnter()
    {
        Hunter.instance.renderer.material.color = Color.green;
        cooldown = 0;
    }

    public override void OnUpdate()
    {
        Vector2 dist = Hunter.instance.transform.position - Hunter.instance._target.transform.position;
        if (cooldown <= restCooldown)
        {
            cooldown += Time.deltaTime;
        }
        else if (cooldown > restCooldown)
        {
            cooldown = 0;
            Hunter.instance.energy = 10;
        }
        if (Hunter.instance.energy > 0)
        {
            if (dist.magnitude < Hunter.instance._radius)
            {
                fsm.ChangeState(EnemyActions.Chase);
            }
            else
            {
                fsm.ChangeState(EnemyActions.Patrol);
            }
        }
    }

    public override void OnExit()
    {
    }


}