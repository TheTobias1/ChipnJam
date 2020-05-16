using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpPlayer : MonoBehaviour
{
    public List<AudioClip> theLoops;
    public Disc discPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PlayNewDisc(theLoops[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNewDisc(AudioClip clip){
        Disc disc = Instantiate(discPrefab);
        disc.StartMusic(clip);

    }


}
