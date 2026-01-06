using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour


{

    KeyCode nextKey;
    public TextAsset textAsset;
    public TextMeshProUGUI keyTextDisplay;
    public TextMeshProUGUI playerTextDisplay;
    public int charsTyped = 0;
    public int offset = 11;
    

    string currentText;
    char nextChar;
    string playerText = "_";

    float audienceMax = 100f;
    float audienceMin = 0.0f;
    public float audienceScore = 50.0f;
    public float penalty = 4f;
    float boost = 2f;
    float decay = 10f;
    public Image repBar;

    public int score = 0;
    public ModelSpawner modelspawner;
    public bool glamming = false;
    public int glamCount = 0;

    public bool gameOver = false;
    public bool finished = false;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI goodEnding;
    public TextMeshProUGUI badEnding;
    public TextMeshProUGUI scoreEnding;
    public int maxLength;
    AudioSource audioSource;
    public AudioClip cheer;
    public AudioClip gasp;
    public AudioClip cameraClick;
    public AudioClip boo;

    // Start is called before the first frame update
    void Start()
    {
        //currentText = Resources.Load<TextAsset>("text.txt").text;
        currentText = textAsset.text.ToUpper();
        nextChar = currentText[offset];
        keyTextDisplay.text = currentText.Substring(charsTyped, charsTyped + 20);
        playerTextDisplay.text = playerText;
        maxLength = currentText.Length;
        gameOverCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!gameOver)
        {
            //MODELS ON RUNWAY
            string _string = Input.inputString.ToUpper();
            if (_string != "")
            {


                if (CorrectButtonDown(_string))
                {

                    if (!glamming)
                    {
                        modelspawner.MoveAllModels();
                    }
                    else
                    {
                        if (_string.Contains(" "))
                        {
                            modelspawner.outgoingModel.Glam();
                            glamCount++;
                            audioSource.clip = cameraClick;
                            audioSource.Play();
                            if (glamCount == 3) { glamming = false; glamCount = 0; score += 10; modelspawner.outgoingModel.glammed = true; audioSource.clip = cheer; audioSource.Play(); }
                        }

                    }
                    audienceScore += boost;
                    score++;
                    Debug.Log("Good!");
                    if (score % 20  == 0) {
                        audioSource.clip = cameraClick;
                        audioSource.Play();
                    }

                }
                else
                {
                    modelspawner.outgoingModel.Stumble();
                    audioSource.clip = gasp;
                    audioSource.Play();
                    audienceScore -= penalty;
                    penalty = penalty * 1.01f;
                    Debug.Log("Bad! Expecting " + nextChar);
                }

                //TODO: Append input string to player input, then advance both typewriter lines

                foreach (char c in _string)
                {
                    if (c != '\b' && c != '\n')
                    {

                        playerText = playerText.Substring(0, playerText.Length - 1);
                        playerText += c;
                        playerText += "_";
                        playerText = playerText.Replace(" ", "\u00A0");
                        UpdateCharacter();
                    }
                }

                Debug.Log(playerText.ToString());
            }
            //TODO: See if a model has left the stage

            //AUDIENCE MECHANIC

            audienceScore -= Time.deltaTime * decay;
            audienceScore = Mathf.Clamp(audienceScore, audienceMin, audienceMax);
            UpdateReputationBar();
            if (audienceScore == audienceMin) { gameOver = true; scoreEnding.text += score.ToString(); }


          
            keyTextDisplay.text = currentText.Substring(charsTyped, 20);
            string _pt = playerText.Substring(math.max(playerText.Length - 11, 0), math.min(playerText.Length, 11));
            playerTextDisplay.text = _pt;

            if (playerText.Length+10 >= maxLength) { finished = true; gameOver = true; scoreEnding.text += score.ToString(); }

        }

        #region gameover
        else
        {
            gameOverCanvas.SetActive(true);
            
            if (!finished)
            {
                Color _color = goodEnding.color;
                _color.a = 0;
                goodEnding.color = _color;
                audioSource.clip = boo;
                audioSource.Play();

            }
            else
            {
                Color _color = badEnding.color;
                _color.a = 0;
                badEnding.color = _color;
                audioSource.clip = cheer;
                audioSource.Play();
            }
            if (Input.GetKey(KeyCode.R))
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        #endregion

    }


    public bool CorrectButtonDown(string _bdstring)
    {
        bool _correct = false;
        
        //string _bdstring = Input.inputString;
        foreach (char c in _bdstring)
        {
            if (c == nextChar)
            {
                _correct = true;
                break;
            }
        }


        return _correct;
    }

    public void UpdateCharacter()
    {
        
        currentText = currentText.Substring(1);
        nextChar = currentText[offset];

        //This function pops the necessary character off the key text, and updates the nextKey variable
    }

    public void UpdateReputationBar()
    {
        float ratio = audienceScore / audienceMax;
        repBar.fillAmount = ratio;
        if (ratio > .5)
        {
            repBar.color = Color.green;
        }
        else if (ratio > .3)
        {
            repBar.color = Color.yellow;
        }
        else { repBar.color = Color.red;}
    }
}
