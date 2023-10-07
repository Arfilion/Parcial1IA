using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Subject
{
    public GameObject _prey;
    public float energy;
    public float maxEnergy=10;
    public static Hunter instance;
    [SerializeField] float _radius;

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
        if (energy <= 0) EventHasTriggered(EnemyActions.Chase);
        if (Input.GetKeyDown(KeyCode.K))
        {
            energy -= 1;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
