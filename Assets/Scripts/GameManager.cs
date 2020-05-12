﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    float xRange = 35;
    float zRange = 15;
    [SerializeField] int wave;
    [SerializeField] int minPeoplePerWave;
    [SerializeField] int minInfectedNumber;
    [SerializeField] GameObject person;

    List<GameObject> peoplePool;
    [SerializeField] int maxPool;

    List<GameObject> people;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePeople();
        NewWave();
    }

    void NewWave()
    {
        // Reset people
        people.Clear();
        peoplePool.ForEach(p => p.SetActive(false));
        PreparePeople(minPeoplePerWave + wave, minInfectedNumber + wave);

    }


    public void PreparePeople(int n, int maxInfected)
    {
        if (n > peoplePool.Count)
        {
            n = peoplePool.Count;
        }
        //List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            // Preparing person
            PersonController pc = peoplePool[i].GetComponent<PersonController>();
            if (i < maxInfected)
            {
                pc.infected = true;
            }
            else
            {
                pc.infected = false;
            }
            pc.transform.position = GenerateSpawnPosition();
            peoplePool[i].SetActive(true);
            people.Add(peoplePool[i]);
        }
    }


    Vector3 GenerateSpawnPosition()
    {
        float x = Random.Range(-xRange, xRange);
        float z = Random.Range(-zRange, zRange);
        return new Vector3(x, 1, z);
    }

    void GeneratePeople()
    {
        peoplePool = new List<GameObject>();
        people = new List<GameObject>();
        Vector3 initPosition = new Vector3(-1000, -1000, -1000);
        for (int i = 0; i < maxPool; i++)
        {
            GameObject aPerson = Instantiate(person, initPosition, person.transform.rotation);
            peoplePool.Add(aPerson);
        }
    }

    bool IsGameOver()
    {
        return (people.FindAll(p => p.gameObject.GetComponent<PersonController>().IsHealthy()).Count == people.Count);
    }
}
