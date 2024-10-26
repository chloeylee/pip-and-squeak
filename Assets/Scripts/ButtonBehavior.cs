using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // stepping on button opens door
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }

    // stepping off button closes door
    private void OnTriggerExit2D(Collider2D other)
    {
        // TODO: maybe add a delay/timer to keep door open for a bit
        // and/or add an animation
        if (other.gameObject.tag == "Player")
        {
            door.SetActive(true);
        }
    }
}
