using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    private float timeSinceStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = SimpleSceneManager.Instance.loadingPosition;
        this.transform.rotation = Quaternion.Euler(SimpleSceneManager.Instance.loadingRotation);
        this.transform.localScale = SimpleSceneManager.Instance.loadingScale;
    }

    void Update()
    {
        

        this.transform.Rotate(0, 0, -0.1f);

        transform.localScale -= new Vector3(0.01f, 0.01f, 0);
        timeSinceStart += Time.deltaTime;
        if (timeSinceStart >= 5f)
        {
            Destroy(gameObject);
        }



    }

}
