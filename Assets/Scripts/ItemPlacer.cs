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

    private bool isPlacingPlayer = false;
    private bool isPlacingMagnet = false;

    [SerializeField] private int maxMagnetCount;
    private int magnetCount = 0;
    [SerializeField] private TextMeshProUGUI magnetText;
    
    void Start()
    {
        UpdateText(magnetCount, maxMagnetCount);
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
                    }
                }
            }
        } else if (isPlacingMagnet)
        {
            magnet.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0))
            {
                if (playerConstraint == null) {
                    isPlacingMagnet = false;
                    magnetCount++;
                    UpdateText(magnetCount, maxMagnetCount);
                } else {
                    if (!playerConstraint.bounds.Contains(mousePos)) {
                        isPlacingMagnet = false;
                        magnetCount++;
                        UpdateText(magnetCount, maxMagnetCount);
                    }
                }

            }
        }



    }

    // place player prefab
    public void PlacePlayer()
    {
        if (player != null) return;
        player = Instantiate(playerPrefab, mousePos, Quaternion.identity);
        isPlacingPlayer = true;
    }

    // place magnet prefab
    public void PlaceMagnet()
    {
        if (magnetCount >= maxMagnetCount) return;
        magnet = Instantiate(magnetPrefab, mousePos, Quaternion.identity);
        isPlacingMagnet = true;
    }

    private void UpdateText(int magnetCount, int maxMagnetCount)
    {
        magnetText.text = "Magnets:" + magnetCount + "/" + maxMagnetCount;
    }


}
