using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{

    public model modelPrefab;
    public model[] models;
    public GameObject[] waypoints;
    public model outgoingModel;
    public model returningModel;
    public int pathSteps = 30;
    public float frequency = .5f;
    public float amplitude = .5f;
    public GameManager manager;

    public GameObject spawnPoint;



    // Start is called before the first frame update
    void Start()
    {
        SpawnModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnModel()
    {
       model _mo = Instantiate(modelPrefab, waypoints[0].transform.position,Quaternion.identity,this.transform);
       _mo.waypoints = waypoints;
       _mo.goal = waypoints[1];
       _mo.ChangeColors();
        models.Append(_mo);
        _mo.spawner = this;
        outgoingModel = _mo;
        _mo.pathSteps = pathSteps;
        _mo.frequency = frequency;
        _mo.amplitude = amplitude;
        _mo.manager = manager;
    }

    public void MoveAllModels()
    {
        if (returningModel != null)  returningModel.Move();
        if (outgoingModel != null) outgoingModel.Move();
    }
}
