using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSequence : MonoBehaviour
{
    public GameObject player;

    public bool leaving = false;
    float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLevel());
        PlayerMovement.playerFrozen = true;
    }

    IEnumerator StartLevel()
    {
        Vector3 target = transform.position;
        transform.position = target - transform.forward * 25;

        while(Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 t = Vector3.Lerp(transform.position, target, 14 * Time.deltaTime);
            Vector3 vel = t - transform.position;
            vel = Vector3.ClampMagnitude(vel, 8 * Time.deltaTime);

            transform.Translate(vel, Space.World);
            yield return null;
        }

        transform.position = target;

        player.transform.parent = null;
        PlayerMovement.playerFrozen = false;
    }

    private void Update()
    {
        if(leaving)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            speed = Mathf.Min(speed + 20 * Time.deltaTime, 8);
            transform.Rotate(new Vector3(0, -4 * Time.deltaTime, 0));
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player" && EnemySpawner.islandComplete && !leaving)
        {
            leaving = true;
            player.transform.parent = transform;
            PlayerMovement.playerFrozen = true;
            Invoke("NextScene", 3);
        }
    }

    public void NextScene()
    {
        SessionManager.manager.LoadMusicLevel();
    }


}
