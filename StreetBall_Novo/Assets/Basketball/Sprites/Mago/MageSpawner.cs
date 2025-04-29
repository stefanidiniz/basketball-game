using UnityEngine;
using System.Collections;

public class MagoSpawner : MonoBehaviour
{
    public GameObject magoPrefab;
    public float delayInicial = 3f;
    public float intervalo = 10f;

    private Camera cam;
    private float zDistance = 10f;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(LoopSpawn());
    }

    IEnumerator LoopSpawn()
    {
        yield return new WaitForSeconds(delayInicial);
        while (true)
        {
            SpawnarMago();
            yield return new WaitForSeconds(intervalo);
        }
    }

    void SpawnarMago()
    {
        Vector3 startPosition = cam.ViewportToWorldPoint(new Vector3(0.5f, -0.2f, zDistance));
        Instantiate(magoPrefab, startPosition, Quaternion.identity);
    }
}
