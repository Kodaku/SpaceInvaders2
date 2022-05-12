using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    public GameObject start;
    // Start is called before the first frame update
    public void DeactivateStart()
    {
        start.SetActive(false);
    }

    public void StartGame()
    {
        start.SetActive(true);
        GameManager.IsMovementEnabled = true;
    }
    
    public void AutoDestroy()
    {
        Destroy(this.gameObject);
    }
}
