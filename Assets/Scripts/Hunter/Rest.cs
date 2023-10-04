using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : State
{
    public override void OnEnter()
    {
        Debug.Log("Entre a Rest");
    }

    public override void OnUpdate()
    {
        Debug.Log("Estoy en Rest");
    }

    public override void OnExit()
    {
        Debug.Log("Sali de Rest");
    }

    public IEnumerator RestProcedure()
    {
        yield return new WaitForSeconds(3f);
        Hunter.instance.energy = Hunter.instance.maxEnergy;
    }
}
