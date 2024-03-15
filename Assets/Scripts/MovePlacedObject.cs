using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


enum ItemType {
    Player,
    Magnet,
    VelocityIndicator
}

public class MovePlacedObject : MonoBehaviour
{
    [HideInInspector] public bool isDragging;
    private Vector3 mouseOffset;
    public bool canBeMoved = false;
    private velocityIndicatorPlacer indicatorPlacer;
    private GameManagement gameManagement;

    [SerializeField] private ItemType itemType;

    void Start()
    {
        indicatorPlacer = GameObject.Find("GameManager").GetComponent<velocityIndicatorPlacer>();
        gameManagement = GameObject.Find("GameManager").GetComponent<GameManagement>();
    }

    private void OnMouseDown()
    {
        if (!canBeMoved || gameManagement.isGameRunning) return;


        isDragging = true;

        // calculate the offset between the mouse cursor position and the sprite's position
        mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseUp()
    {
        if (!canBeMoved || gameManagement.isGameRunning) return;

        isDragging = false;

        if (itemType == ItemType.Player){
            indicatorPlacer.placeIndicator();
        }
    }

    private void Update()
    {
        if (!canBeMoved || gameManagement.isGameRunning) return;

        if (isDragging)
        {
            if (itemType == ItemType.Player){
                // delete the indicator and spawn a new one on mouse up to resize it
                GameObject indicator = GameObject.Find("initialVelocityIndicator(Clone)");
                if (indicator != null) {
                    Destroy(indicator);
            }
        }
            // adjust the sprite position with respect to the mouse's position and the original offset
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseOffset;
            newPosition.z = 0; // Keep z-coordinate constant, as we're in a 2D space
            transform.position = newPosition;
        }
    }
}
