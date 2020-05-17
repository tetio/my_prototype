﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] GameObject firedMedecine;

    float zRange = 20f;
    float xRange = 40f;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsDead())
        {
            float hMov = Input.GetAxis("Horizontal");
            float vMov = Input.GetAxis("Vertical");
            if (hMov != 0 || vMov != 0)
            {
                InsideBounds();
                transform.Translate(new Vector3(hMov, 0, vMov) * speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                firedMedecine.transform.position = gameObject.transform.position;
                firedMedecine.gameObject.SetActive(true);
                StartCoroutine(FiredMedecineCountdown());
            }
        }
    }

    IEnumerator FiredMedecineCountdown()
    {
        yield return new WaitForSeconds(3);
        firedMedecine.gameObject.SetActive(false);

    }

    void InsideBounds()
    {
        if (transform.position.x >= xRange)
        {
            transform.position = new Vector3(xRange, 0, transform.position.z);
        }
        if (transform.position.x <= -xRange)
        {
            transform.position = new Vector3(-xRange, 0, transform.position.z);
        }
        if (transform.position.z >= zRange)
        {
            transform.position = new Vector3(transform.position.x, 0, zRange);
        }
        if (transform.position.z <= -zRange)
        {
            transform.position = new Vector3(transform.position.x, 0, -zRange);
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    private void OnCollisionEnter(Collision collision)
    //private void OnTriggerEnter(Collider )
    {
        GameObject other = collision.gameObject;
        if (other.gameObject.CompareTag("Person"))
        {
            PersonController pc = other.gameObject.GetComponent<PersonController>();
            if (pc.IsInfected())
            {
                dead = true;
            }
            else
            {
                Debug.Log("hit a " + other.name);
            }
        }
    }
}