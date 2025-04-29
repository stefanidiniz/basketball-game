// r0b3rT
// 2025-04-25

using UnityEngine;

public class ZonaDaCestaDetector : MonoBehaviour
{
    // ======================== VARI�VEIS ========================

    private PlayerRandomShot shotScript;

    // ======================== UNITY M�TODOS ========================

    // busca o script PlayerRandomShot no mesmo objeto
    void Start()
    {
        shotScript = GetComponent<PlayerRandomShot>();
    }

    // detecta entrada na �rea da cesta (via trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ZonaCesta") && shotScript != null)
        {
            shotScript.dentroDaZonaDaCesta = true;
            Debug.Log("Entrou na zona da cesta");
        }
    }

    // detecta sa�da da �rea da cesta (via trigger)
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ZonaCesta") && shotScript != null)
        {
            shotScript.dentroDaZonaDaCesta = false;
            Debug.Log("Saiu da zona da cesta");
        }
    }
}