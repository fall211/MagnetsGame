using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMagnetText : MonoBehaviour
{
    public void UpdateText(int magnetCount, int maxMagnetCount)
    {
        GetComponent<TextMeshProUGUI>().text = "Magnets:" + magnetCount + "/" + maxMagnetCount;
    }
}
