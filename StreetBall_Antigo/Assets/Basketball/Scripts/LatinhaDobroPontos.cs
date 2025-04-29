using UnityEngine;

public class LatinhaDobroPontos : MonoBehaviour
{
    [SerializeField] private float duracaoEfeito = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                //player.AtivarPontuacaoMultiplicada(duracaoEfeito);
            }

            Destroy(gameObject);
        }
    }
    /*if (player.EstaComPontuacaoMultiplicada())
    {
    pontos *= 2;
    }*/
}
