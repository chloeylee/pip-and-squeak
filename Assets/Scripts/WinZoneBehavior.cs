using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZoneBehavior : MonoBehaviour
{
    public GameObject winScreen;

    public GameObject pip;
    public GameObject squeak;

    private bool pipInZone = false;
    private bool squeakInZone = false;

    private void Start()
    {
        winScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if Player 1 has entered the area
        if (other.gameObject == pip)
        {
            pipInZone = true;
        }

        // Check if Player 2 has entered the area
        if (other.gameObject == squeak)
        {
            squeakInZone = true;
        }

        // Check if both players are in the area
        if (pipInZone && squeakInZone)
        {
            WinGame();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If a player leaves the area, update their status
        if (other.gameObject == pip)
        {
            pipInZone = false;
        }

        if (other.gameObject == squeak)
        {
            squeakInZone = false;
        }
    }

    private void WinGame()
    {
        Debug.Log("You win!");
        winScreen.SetActive(true);

    }
}
