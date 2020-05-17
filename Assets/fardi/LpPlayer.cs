using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LpPlayer : MonoBehaviour
{
    public static bool currentlyPlayingDisc;
    public static List<Abilities> acquiredAbilities;
    static int discsToPlay;
    [SerializeField] int playedDiscsCount;
    [SerializeField] bool waitingToPlayMusic;
    [SerializeField] bool gameHasStarted;
    public static List<Abilities> acquiredLPs;
    public List<AudioClip> theLoops;
    public AudioSource audioSource;
    public Disc discPrefab;
    Disc currentDisc;
    public GameObject newObject;
    // Start is called before the first frame update
    void Start()
    {
        acquiredAbilities = new List<Abilities>();
        Debug.Log("the length of the list:" + theLoops.Count.ToString());
        if (acquiredLPs == null){
            Debug.Log("LPs not provided");
            acquiredLPs = new List<Abilities>(new Abilities[] { 
                    Abilities.PeaShooter, 
                    Abilities.Orbgun,
                    Abilities.Denial
                });
        }
        else if (acquiredLPs.Count == 0){
            EndRhythmGame();
        }

        LpPlayer.discsToPlay = acquiredLPs.Count;
        
        StartCoroutine(PlayNewDisc(theLoops[(int)acquiredLPs[playedDiscsCount]], 1f));
    }

    // Update is called once per frame
    void Update()
    {
        //Check here if a rhythm game has been finished before starting the next
        if(!LpPlayer.currentlyPlayingDisc && !waitingToPlayMusic){
            if (playedDiscsCount > 0){
                if (Disc.playerWon)
                {
                    acquiredAbilities.Add(acquiredLPs[playedDiscsCount]);
                    Disc.playerWon = false;
                }
            }
            if (playedDiscsCount >= LpPlayer.discsToPlay-1 && !waitingToPlayMusic)
            {
                if (newObject != null)
                {
                    EndRhythmGame();
                }
            }else
            {
                if (!waitingToPlayMusic) playedDiscsCount++;
                StartCoroutine(PlayNewDisc(theLoops[(int)acquiredLPs[playedDiscsCount]], 1f));
            }
        }
    }

    IEnumerator PlayNewDisc(AudioClip clip, float waitTime){
        waitingToPlayMusic = true;
        audioSource.Play();
        yield return new WaitForSeconds(waitTime);
        currentDisc = Instantiate(discPrefab);
        Debug.Log("Disc has been instantiated");
        currentDisc.StartMusic(clip);
        LpPlayer.currentlyPlayingDisc = true;
        Disc.playerWon = false;
        waitingToPlayMusic = false;
        yield return null;
    }

    void EndRhythmGame(){
        Instantiate(newObject);
        Destroy(gameObject);
    }


}
