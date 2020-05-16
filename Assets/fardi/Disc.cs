using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    [SerializeField] bool loopIsPlaying;
    bool clickWindowActive;
    bool playerMissedBeat;
    bool monitorPlayTime;

    //points management
    public int loseState;
    public int WinState;
    [SerializeField] private int points;

    [SerializeField] private float time;
    [SerializeField] private float loopTime;
    [SerializeField] private float criticalTime;
    public float clickWindowTime;
    int loopCount;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        loopIsPlaying = false;
        clickWindowActive = false;
        monitorPlayTime = false;
        playerMissedBeat = true;
        loopCount = 0;
        time = 0;
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
        
        Debug.Log("PlayRhythmGame called");
        //keep accurate value for time since audio begun playing
        if(!monitorPlayTime){
            monitorPlayTime = true;
        }
        else{
            time += Time.deltaTime;
        }

        if (time >= (loopTime - clickWindowTime) ||
         time <= (loopTime + clickWindowTime))
        {
            if(clickWindowActive == false){
                clickWindowActive = true;
            }
        }
        else{
            //reset bools
            if(clickWindowActive == true){
                clickWindowActive = false;
                loopTime += loopTime;
                if (playerMissedBeat == true){
                    Debug.Log("Player missed the beat");
                    points -= 1;
                }
                else
                {
                    playerMissedBeat = true;
                }
            }
        }

        //now check 
        if(Input.GetKeyDown("jump")){
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
        

    }

    void changeScore(int num){

    }    
}
