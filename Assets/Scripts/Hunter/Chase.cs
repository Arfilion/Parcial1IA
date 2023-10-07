using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Transform _target;
    Renderer _rend;
    Hunter _hunter;



    public Chase(Hunter p)
    {
        _target = p.transform;
        _rend = p.GetComponent<Renderer>();

    }

    public override void OnEnter()
    {
        Debug.Log("Entre a Chase");
        _rend.material.color = Color.red;

    }

    public override void OnUpdate()
    {
        //_hunter.transform.position += new Vector3(1, 0);
        if (Hunter.instance.energy <= 0)
        {
            fsm.ChangeState(EnemyActions.Rest);
        }
        Debug.Log("Estoy en Chase");
    }

    public override void OnExit()
    {
        Debug.Log("Sali de Chase");
        _rend.material.color = Color.white;

    }

    
}
