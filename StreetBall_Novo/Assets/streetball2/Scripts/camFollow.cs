using System.Collections;
using System.Collections.Generic;
// using System.Numerics; // Remove this line as it is not needed
using UnityEngine;

public class camFollow : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] Vector2 distance;
    [SerializeField] float velocity;
    Vector2 posi;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        posi.x = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref distance.x, velocity);
        posi.y = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref distance.y, velocity);
        transform.position = new Vector3(posi.x, posi.y, transform.position.z);
    }
}
