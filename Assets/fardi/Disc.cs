using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    [SerializeField] bool loopIsPlaying;
    bool clickWindowActive;
    bool playerMissedBeat;
    bool firstInputMade;
    public static bool playerWon = false;
    private float buttonDelay;

    //points management
    public int loseState;
    public int winState;
    [SerializeField] private int points;

    [SerializeField] private float time;
    [SerializeField] private float initTime;
    [SerializeField] private float loopTime;
    [SerializeField] private float criticalTime;
    public float clickWindowTime;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        clickWindowActive = false;
        firstInputMade = false;
        playerMissedBeat = true;
        criticalTime = 0;
        time = 0;
        initTime = Time.time;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (loopIsPlaying){
          PlayRhythmGame();
        }

    }

    public void StartMusic(AudioClip clip){
        Debug.Log("StartMusic called");
        audioSource.clip = clip;
        loopTime = audioSource.clip.length;
        audioSource.Play();
        loopIsPlaying = true;
    }


    void PlayRhythmGame(){
        //keep accurate value for time since audio begun playing
        /*if(!monitorPlayTime){
            time = Time.time - initTime;
            monitorPlayTime = true;
        }
        else{
            time = Time.time 
        }*/
        time = Time.time - initTime;

        if (time >= (criticalTime - clickWindowTime) &&
         time <= (criticalTime + clickWindowTime))
        {
            if(clickWindowActive == false){
                clickWindowActive = true;
                Debug.Log("Click window is active");
            }
        }
        else{
            //reset bools
            if(clickWindowActive == true){
                clickWindowActive = false;
                Debug.Log("Click Window is inactive");
                criticalTime += loopTime;
                if (playerMissedBeat == true && firstInputMade == true){
                    Debug.Log("Player missed the beat");
                    points -= 1;
                }
                else
                {
                    playerMissedBeat = true;
                }
            }
        }

        //now check for input, and timing success
        if(Input.GetButton("RhythmHit") && Time.time > buttonDelay 
        && Time.time >= (initTime + loopTime - clickWindowTime)){
            buttonDelay = Time.time + 0.3f;
            firstInputMade = true;
            if (clickWindowActive == true){
                Debug.Log("successful click");
                points += 1;
                playerMissedBeat = false;
            }
            else {
                Debug.Log("bad click");
                points -= 1;
            };
        }

        if (points >= winState){
            Debug.Log("Player won");
            Disc.playerWon = true;
            LpPlayer.currentlyPlayingDisc = false;
            Destroy(gameObject);
        }
        else if (points <= loseState){
            Debug.Log("Player lost");
            Disc.playerWon = false;
            LpPlayer.currentlyPlayingDisc = false;
            Destroy(gameObject);

        }
        

    }

    
}
