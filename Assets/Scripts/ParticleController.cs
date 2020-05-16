using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }



    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Person"))
        {
            PersonController pc = other.gameObject.GetComponent<PersonController>();
            if (pc.infected)
            {
                pc.Recover();
            }
        }
    }
}
