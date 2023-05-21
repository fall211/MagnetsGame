using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    [SerializeField] private GameObject objectPlacer;
    [SerializeField] private GameObject startButton;

    private velocityIndicatorPlacer indicatorPlacer;

    private Vector3 initialPlayerPos;

    public bool isGameRunning = false;

    void Start()
    {
        indicatorPlacer = objectPlacer.GetComponent<velocityIndicatorPlacer>();
    }

    public void startGame(){
        isGameRunning = true;
        // disable constraints, enable magnetism, apply initial velocity
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<ConstrainPlayer>().enabled = false;
        player.GetComponent<Player>().enabled = true;
        player.GetComponent<Rigidbody2D>().velocity = indicatorPlacer.velocity;
        indicatorPlacer.indicator.SetActive(false);

        initialPlayerPos = player.transform.position;

        // disable the button
        startButton.SetActive(false);
    }

    public void resetLevel(){
        isGameRunning = false;
        // reset the game
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        Destroy(player);

        objectPlacer.GetComponent<ItemPlacer>().resetPlayer(initialPlayerPos);

        indicatorPlacer.indicator.SetActive(true);

        // enable the button
        startButton.SetActive(true);
    }

}
