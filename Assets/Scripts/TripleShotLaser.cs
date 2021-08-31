using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotLaser : MonoBehaviour
{
    [SerializeField]
    private float _TripleLaserSpeed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _TripleLaserSpeed * Time.deltaTime);

        if (transform.position.y > 8.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
