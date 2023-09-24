using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour


{

    KeyCode nextKey;
    string currentText;

    float audienceScore = 50.0f;
    float penalty = 0.1f;
    float boost = 


    // Start is called before the first frame update
    void Start()
    {
        //TODO: Initialize the variable currentText
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Create restart button
        
        //MODELS ON RUNWAY
        //TODO: Find out what key is being expected
        //TODO: If the expected key is being depressed, move all active models forward and/or make them pose. Also, increment the crowd score
        //TODO: See if a model has left the stage

        //AUDIENCE MECHANIC
        //TODO: Decrease the audience score a little bit—they grow bored over time
        


    }


    public bool CorrectButtonDown()
    {
        bool _correct = false;
        //TODO: Get passed the corresponding KeyCode for the next symbol that's necessary, and then check to see if that key is down
        return _correct;
    }

    public void UpdateCharacter()
    {
        //This function pops the necessary character off the key text, and updates the nextKey variable
    }
}
