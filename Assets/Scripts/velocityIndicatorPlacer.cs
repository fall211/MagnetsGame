using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityIndicatorPlacer : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrefab;

    private Vector3 mousePos;
    private bool isPlacingIndicator = false;
    public GameObject indicator;
    private GameObject player;
    public Vector3 velocity;


    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (isPlacingIndicator){
            Vector3 direction = mousePos - player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            // limit the indicator's length
            if (direction.magnitude > 5){
                direction = direction.normalized * 5;
            }

            indicator.transform.localScale = new Vector3(1, direction.magnitude, 1);
            // move the indicator so the base is at the player
            // limit the indicator from going too far
            Vector3 indicatorPos = direction.normalized * (direction.magnitude / 2);

            indicator.transform.position = player.transform.position + indicatorPos;
            if (Input.GetMouseButtonDown(0))
            {
                isPlacingIndicator = false;
                velocity = direction;
            }
        }

    }

    public void placeIndicator(){
        indicator = Instantiate(indicatorPrefab, player.transform.position, Quaternion.identity);
        isPlacingIndicator = true;
    }

    public void deleteIndicator(){
        Destroy(indicator);
    }

    public void setPlayer(GameObject player){
        this.player = player;
    }
}
