using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class model : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject top;
    public GameObject middle;
    public GameObject bottom;
    public GameObject[] waypoints;
    public GameObject goal;
    public ModelSpawner spawner;
    public GameManager manager;
    public int origin = 0;
    public int pathSteps;
    public int stepsTaken = 0;
    public int glamSteps = 0;
    public bool glammed = false;
    public float frequency = .5f;
    public float amplitude = .5f;
    public bool stumbling = false;
    public int stumbleCount = 0;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(GameObject _go)
    {
        SpriteRenderer _sr = _go.GetComponent<SpriteRenderer>();
        _sr.color = Random.ColorHSV();
    }

    public void ChangeColors()
    {
        ChangeColor(top);
        ChangeColor(middle);
        ChangeColor(bottom);
    }

    public void Move()
    {
        if (stumbling)
        {
            transform.eulerAngles += new Vector3(0, 0, 90);
            stumbling = false;
        }
        //transform.position = Vector2.Lerp(waypoints[origin].transform.position, waypoints[origin + 1].transform.position, (float)stepsTaken / pathSteps);
        Vector2 mainPosition = Vector2.Lerp(waypoints[origin].transform.position, waypoints[origin + 1].transform.position, (float)stepsTaken / pathSteps);
        float upDown = Mathf.Sin((float)stepsTaken * frequency) * amplitude; // frequency and amplitude control the wave's speed and height
        Vector2 upDownVector = new Vector2(0, upDown);

        transform.position = mainPosition + upDownVector;

        stepsTaken++;
        if (stepsTaken >= pathSteps)
        {
            Turn();
        }
    }

    public void Turn()
    {
        if (origin == 1) { 
            Destroy(this.gameObject);
            spawner.returningModel = null;

        }
        else
        {
            if (glammed)
            {
                origin = origin + 1;
                stepsTaken = 0;
                spawner.returningModel = this;
                spawner.SpawnModel();
            }
            else if (!manager.glamming) { manager.glamming = true; }
        }
    }

    public void Glam()
    {
        //TODO: implement sound for glamming
        ChangeColors();

    }

    public void Stumble()
    {
        if (!stumbling)
        {
            //TODO: implement stumbling audio
            transform.eulerAngles += new Vector3(0, 0, -90);
            stumbling = true;
            stumbleCount++;
            manager.score -= 1 * stumbleCount;
            
        }

    }

}
