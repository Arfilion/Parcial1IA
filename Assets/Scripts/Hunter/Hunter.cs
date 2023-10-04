using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Subject
{
    public float energy;
    public float maxEnergy=10;
    public static Hunter instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        energy = maxEnergy;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (energy <=0) EventHasTriggered(EnemyActions.Rest);
        if (Input.GetKeyDown(KeyCode.K))
        {
            energy -= 1;
        }

    }

    
}
