﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadController : MonoBehaviour {

    public float speed = 100;
    public float hazardSpeed = 20;
    public float otherCarSpeed = 10;
    public GameObject segment;
    public GameObject[] cars;
    public GameObject[] hazards;
    public GameObject[] environmentPieces;
    public GameObject alternate;
    public int numSegments = 16;

    float segmentLength;
    float trackLength;

    Transform roadPool;
    Transform hazardPool;
    Transform carPool;

    public float score;
    public int multiplier = 1;
    float highScore;
    Text scoreDisplay;
    Text highScoreDisplay;

    private void Start()
    {
        hazardPool = transform.GetChild(0);
        carPool = transform.GetChild(1);
        roadPool = transform.GetChild(2);

        segmentLength = segment.GetComponent<Renderer>().bounds.size.z;
        trackLength = segmentLength * numSegments;
        for (int i = 0; i < numSegments; i++)
        {
            //Transform tempSegment = (i %2 == 0 ? Instantiate(segment, roadPool).transform : Instantiate(alternate, roadPool).transform);
            Transform tempSegment = Instantiate(segment, roadPool).transform;
            tempSegment.position = new Vector3(0, 0, i * segmentLength);
            
                
        }
        
    }

    float counter = 0;
    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 5)
        {
            counter = 0;
            PlaceHazard();
        }

        foreach(Transform child in roadPool)
        {
            child.localPosition = new Vector3(0, 0, child.localPosition.z - Time.deltaTime * speed);
            if (child.localPosition.z <= 0)
            {
                child.localPosition = new Vector3(0, 0, child.localPosition.z + trackLength);
            }
        }
        foreach(Transform child in hazardPool)
        {
            child.localPosition = new Vector3(0, 0, child.localPosition.z - Time.deltaTime * hazardSpeed);
            if (child.localPosition.z <= 0)
            {
                RecycleObject(child.gameObject, hazardPool);
            }
        }
    }

    void PlaceHazard()
    {
        Transform tempHazard = Instantiate(hazards[Random.Range(0, hazards.Length)], hazardPool).transform;
        tempHazard.position = new Vector3(0, 0, trackLength);
        tempHazard.rotation = Camera.main.transform.rotation;
    }

    void RecycleObject(GameObject toRecycle, Transform pool)
    {
        toRecycle.SetActive(false);
    }
}
