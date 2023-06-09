﻿using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrainController : MonoBehaviour
{
    public GameObject TrainPrefab;

    private GameObject _myTrain;

    private float _speedMultiplier1 = 1;
    private float _speedMultiplier2 = 1;
    public float SpeedMultiplier
    {
        get
        {
            return (_speedMultiplier1 + _speedMultiplier2) / 2;
        }
    }
    public Vector3 IdealWorldDirection = Vector3.zero;

    public GameObject MakeTrain()
    {
        _myTrain = Instantiate(TrainPrefab);

        return _myTrain;
    }

    void OnMove(InputValue val)
    {
        Vector2 rawInput = val.Get<Vector2>();
        Debug.Log(rawInput);

        IdealWorldDirection = FindObjectOfType<Camera>().transform
            .TransformDirection(new Vector3(rawInput.x, rawInput.y, 0));
    }

    void OnSlowdown1(InputValue val)
    {
        _speedMultiplier1 = 1 - val.Get<float>();
    }

    void OnSlowdown2(InputValue val)
    {
        _speedMultiplier2 = 1 - val.Get<float>();
    }

    private void Update()
    {
        if (_myTrain == null)
        {
            return;
        }

        TrainMovement movement = _myTrain.GetComponent<TrainMovement>();

        movement.TravelSpeed = movement.MaxTravelSpeed * SpeedMultiplier;
        movement.IdealTurningDirection = IdealWorldDirection;
    }
}