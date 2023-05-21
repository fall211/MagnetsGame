using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityIndicatorPlacer : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrefab;

    private Vector3 mousePos;
    private bool isPlacingIndicator = false;
    private GameObject indicator;
    private GameObject player;


    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (isPlacingIndicator){
            Vector3 direction = mousePos - player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            indicator.transform.localScale = new Vector3(1, direction.magnitude, 1);
            // move the indicator so the base is at the player
            indicator.transform.position = mousePos - (direction / 2);
            if (Input.GetMouseButtonDown(0))
            {
                isPlacingIndicator = false;
            }
        }

    }

    public void placeIndicator(){
        indicator = Instantiate(indicatorPrefab, player.transform.position, Quaternion.identity);
        isPlacingIndicator = true;
    }

    public void setPlayer(GameObject player){
        this.player = player;
    }
}
