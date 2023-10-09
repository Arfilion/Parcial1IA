using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject objectToSpawn; //zanahorias a instanciar
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // OnDestroy();
    }
    private void OnDestroy()
    {
        if (objectToSpawn == null)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}
