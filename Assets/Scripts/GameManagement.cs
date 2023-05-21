using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    [SerializeField] private GameObject objectPlacer;
    [SerializeField] private GameObject startButton;

    public bool isGameRunning = false;


    public void startGame(){
        isGameRunning = true;
        // disable constraints, enable magnetism, apply initial velocity
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<ConstrainPlayer>().enabled = false;
        player.GetComponent<Player>().enabled = true;
        player.GetComponent<Rigidbody2D>().velocity = objectPlacer.GetComponent<velocityIndicatorPlacer>().velocity;
        objectPlacer.GetComponent<velocityIndicatorPlacer>().deleteIndicator();

        // disable the button
        startButton.SetActive(false);
    }

    public void resetLevel(){
        isGameRunning = false;
        // reset the game
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        Destroy(player);
        objectPlacer.GetComponent<ItemPlacer>().resetLevel();

        // enable the button
        startButton.SetActive(true);
    }

}
