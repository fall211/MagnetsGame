using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private MovePlacedObject movePlacedObject;

    private velocityIndicatorPlacer indicatorPlacer;
    private ItemPlacer itemPlacer;


    private Vector3 initialPlayerPos;

    public bool isGameRunning = false;

    void Start()
    {
        indicatorPlacer = GetComponent<velocityIndicatorPlacer>();
        itemPlacer = GetComponent<ItemPlacer>();
    }

    void Update(){


        foreach (var obj in GameObject.FindObjectsOfType<MovePlacedObject>()) {
            if (obj.isDragging) {
                disableButtons();
                return;
            }
        }
        if (itemPlacer.isPlacingMagnet || itemPlacer.isPlacingPlayer){
            disableButtons();
            return;
        }
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0){
            disableButtons();
            return;
        }

        if (isGameRunning){
            startButton.SetActive(false);
            resetButton.SetActive(true);
        } else {
            startButton.SetActive(true);
            resetButton.SetActive(false);
        }
        
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

        itemPlacer.resetPlayer(initialPlayerPos);

        indicatorPlacer.indicator.SetActive(true);

        // enable the button
        startButton.SetActive(true);
    }

    public void disableButtons(){
        startButton.SetActive(false);
        resetButton.SetActive(false);
    }

}
