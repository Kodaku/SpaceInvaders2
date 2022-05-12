using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestruction());
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
