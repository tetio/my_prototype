using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{

    private float initialPercentageInfected = 30;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Material illMaterial;
    [SerializeField]
    private Material healthyMaterial;
    [SerializeField]
    private Material deadMaterial;
    [SerializeField]
    private Material recoveredMaterial;
    [SerializeField]
    private GameObject firedMedicine;


    public bool infected;

    private bool recovered;

    private bool dead;
    //[SerializeField]
    private Vector3 dir;
    private MeshRenderer mr;

    private float illTime;




    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        if (Random.Range(0, 100) <= initialPercentageInfected)
        {
            infected = true;
        }
        int xDir = (Random.Range(0, 100) > 50) ? 1 : -1;
        int zDir = (Random.Range(0, 100) > 50) ? 1 : -1;
        dir = new Vector3(xDir, 0, zDir);
        if (infected)
        {
            mr.material = illMaterial;
            illTime = Time.realtimeSinceStartup;
        }
        recovered = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && infected && Time.realtimeSinceStartup - illTime > 100)
        {
            if (Random.Range(0, 100) < 20)
            {
                mr.material = deadMaterial;
                speed = 0;
                dead = true;
            }
            else
            {
                Recover();
            }

        }

        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void Recover()
    {
        recovered = true;
        infected = false;
        mr.material = recoveredMaterial;
    }


    private void OnCollisionEnter(Collision collision)
    //private void OnTriggerEnter(Collider other)
    {
        CollisionDetected(collision.gameObject);
        //CollisionDetected(other.gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    void CollisionDetected(GameObject other)
    {
        if (other.gameObject.transform.name == ("NorthWall"))
        {
            dir = Vector3.Scale(dir, new Vector3(1, 0, -1));
        }
        else if (other.gameObject.transform.name == "SouthWall")
        {
            dir = Vector3.Scale(dir, new Vector3(1, 0, -1));
        }
        else if (other.gameObject.transform.name == "EastWall")
        {
            dir = Vector3.Scale(dir, new Vector3(-1, 0, 1));
        }
        else if (other.gameObject.transform.name == "WestWall")
        {
            dir = Vector3.Scale(dir, new Vector3(-1, 0, 1));
        }
        else if (other.gameObject.transform.name == "Barrier")
        {
            dir = Vector3.Scale(dir, new Vector3(-1, 0, 1));
        }
        else if (other.gameObject.CompareTag("Person"))
        {
            ContactedPerson(other.gameObject.GetComponent<PersonController>());
        }
        else
        {
            Debug.Log("moc moc!");
        }
    }

    void ContactedPerson(PersonController p)
    {
        //PersonController p = other.gameObject.GetComponent<PersonController>();
        if (p.infected && !infected && !recovered && !p.dead)
        {
            infected = true;
            illTime = Time.realtimeSinceStartup;
            speed /= 2;
            mr.material = illMaterial;
        }
        else if (infected && !dead && !p.infected && !p.recovered)
        {
            p.infected = true;
            p.illTime = Time.realtimeSinceStartup;
            p.speed /= 2;
            p.mr.material = illMaterial;
        }
        if (Random.Range(0, 100) <= 20)
        {
            //dir = Vector3.Scale(dir, new Vector3(-1, 0, -1));
            float xDir = (Random.Range(-1, 2) > 0) ? 1 : -1;
            float zDir = (Random.Range(-1, 2) > 0) ? 1 : -1;
            dir = new Vector3(xDir, 0, zDir);
        }
    }

    public bool IsHealthy()
    {
        return !infected;
    }
}
