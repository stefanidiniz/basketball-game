using UnityEngine;

public class HoopTrigger2D : MonoBehaviour
{
    //inicio
    //processamento
    private void OnTriggerEnter2D(Collider2D other)
    {
        // verificar se o objeto que colidiu com o trigger tem a tag "Ball"
        if (other.CompareTag("Ball"))
        {
            Debug.Log("ACERTOU A CESTA!");
        }
    }
}
