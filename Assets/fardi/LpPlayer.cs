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
    //[SerializeField] bool waitingToPlayMusic;
    public static List<Abilities> acquiredLPs;
    public List<AudioClip> theLoops;
    public Disc discPrefab;
    Disc currentDisc;
    public GameObject newObject;
    // Start is called before the first frame update
    void Start()
    {
        acquiredAbilities = new List<Abilities>();
        Debug.Log("the length of the list:" + theLoops.Count.ToString());
        acquiredLPs = new List<Abilities>(new Abilities[] { 
                Abilities.PeaShooter, 
                Abilities.Orbgun,
                Abilities.Denial
            });
        LpPlayer.discsToPlay = acquiredLPs.Count;
        PlayNewDisc(theLoops[(int)acquiredLPs[playedDiscsCount]]);
    }

    // Update is called once per frame
    void Update()
    {
        //Check here if a rhythm game has been finished before starting the next
        if(!LpPlayer.currentlyPlayingDisc){
            if (playedDiscsCount > 0){
                if (Disc.playerWon)
                {
                    acquiredAbilities.Add(acquiredLPs[playedDiscsCount]);
                    Disc.playerWon = false;
                }
            }
            if (playedDiscsCount >= LpPlayer.discsToPlay-1)
            {
                if (newObject != null)
                {
                    Instantiate(newObject);
                }
            }else
            {
                playedDiscsCount++;
                PlayNewDisc(theLoops[(int)acquiredLPs[playedDiscsCount]]);
            }
        }
    }

    void PlayNewDisc(AudioClip clip){
        currentDisc = Instantiate(discPrefab);
        currentDisc.StartMusic(clip);
        LpPlayer.currentlyPlayingDisc = true;
        Disc.playerWon = false;
    }


}
