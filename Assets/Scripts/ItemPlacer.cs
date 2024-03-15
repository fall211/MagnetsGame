using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemPlacer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject magnetPrefab;
    private Vector3 mousePos;
    private GameObject player;
    private GameObject magnet;
    [SerializeField] private BoxCollider2D playerConstraint;
    [HideInInspector] public List<GameObject> magnets;

    [HideInInspector] public bool isPlacingPlayer = false;
    [HideInInspector] public bool isPlacingMagnet = false;

    [SerializeField] private int maxMagnetCount;
    private int magnetCount = 0;
    [SerializeField] private TextMeshProUGUI magnetText;
    [SerializeField] private GameObject magnetParent;
    public velocityIndicatorPlacer indicatorPlacer;
    
    void Start()
    {
        UpdateText(magnetCount, maxMagnetCount);
        if (maxMagnetCount == 0) {
            magnetParent.SetActive(false);
        }
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;


        if (isPlacingPlayer)
        {
            player.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0))
            {
                if (playerConstraint == null) {
                    isPlacingPlayer = false;
                } else {
                    if (playerConstraint.bounds.Contains(mousePos)) {
                        isPlacingPlayer = false;
                        player.GetComponent<ConstrainPlayer>().playerConstraint = playerConstraint;
                    }
                }
                player.GetComponent<MovePlacedObject>().canBeMoved = true;
            }
        } else if (isPlacingMagnet)
        {
            magnet.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0))
            {
                isPlacingMagnet = false;
                magnetCount++;
                UpdateText(magnetCount, maxMagnetCount);
                magnet.GetComponent<MovePlacedObject>().canBeMoved = true;
                magnets.Add(magnet.gameObject);
            }
        }
    }


    // place player prefab
    public void PlacePlayer()
    {
        if (player != null) return;
        player = Instantiate(playerPrefab, mousePos, Quaternion.identity);
        indicatorPlacer.setPlayer(player);
        isPlacingPlayer = true;
    }

    // place magnet prefab
    public void PlaceMagnet()
    {
        if (magnetCount >= maxMagnetCount) return;
        magnet = Instantiate(magnetPrefab, mousePos, Quaternion.identity);
        isPlacingMagnet = true;
    }

    public void resetPlayer(Vector3 pos){
        player = Instantiate(playerPrefab, pos, Quaternion.identity);
        indicatorPlacer.setPlayer(player);
        if (playerConstraint != null) {
            player.GetComponent<ConstrainPlayer>().playerConstraint = playerConstraint;
        }
        player.GetComponent<MovePlacedObject>().canBeMoved = true;
    }

    private void UpdateText(int magnetCount, int maxMagnetCount)
    {
        magnetText.text = "Magnets:" + magnetCount + "/" + maxMagnetCount;
    }


}
