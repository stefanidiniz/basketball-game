using UnityEngine;
using System.Collections;

public class SpawnSystem : MonoBehaviour
{
    public static SpawnSystem Instance;

    [Header("Configurações")]
    public float delayInicial = 10f;
    public float intervaloMeias = 30f;
    public float tempoParaDesaparecer = 20f; // Tempo em segundos até a meia desaparecer se não for pega

    [Header("Prefabs")]
    public GameObject meiaPrefab;
    public GameObject latinhaNormalPrefab;
    public GameObject latinhaNormal2Prefab;
    public GameObject latinhaEspecialPrefab;

    [Header("Área de Jogo")]
    public PolygonCollider2D quadraCollider;

    private GameObject meiaAtual;
    private GameObject latinhaAtual;
    public bool temMeia = false;
    private Coroutine desaparecerCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(RotinaSpawn());
    }

    IEnumerator RotinaSpawn()
    {
        yield return new WaitForSeconds(delayInicial);

        while (true)
        {
            if (!temMeia && meiaAtual == null)
            {
                SpawnMeia();
            }
            yield return new WaitForSeconds(intervaloMeias);
        }
    }

    void SpawnMeia()
    {
        Vector2 posicao = GetPosicaoAleatoriaNaQuadra();
        meiaAtual = Instantiate(meiaPrefab, posicao, Quaternion.identity);

        // Inicia a corotina para fazer a meia desaparecer após o tempo definido
        if (desaparecerCoroutine != null)
        {
            StopCoroutine(desaparecerCoroutine);
        }
        desaparecerCoroutine = StartCoroutine(DesaparecerMeia());
    }

    IEnumerator DesaparecerMeia()
    {
        yield return new WaitForSeconds(tempoParaDesaparecer);

        // Se a meia ainda existir e não foi coletada
        if (meiaAtual != null)
        {
            Destroy(meiaAtual);
            meiaAtual = null;
            SpawnMeia(); // Spawna uma nova meia imediatamente
        }
    }

    public Vector2 GetPosicaoAleatoriaNaQuadra()
    {
        if (quadraCollider == null)
        {
            Debug.LogError("quadraCollider não atribuído no SpawnSystem!");
            return Vector2.zero;
        }

        Bounds bounds = quadraCollider.bounds;
        Vector2 ponto;
        int tentativas = 0;

        do
        {
            ponto = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
            tentativas++;
        } while (!quadraCollider.OverlapPoint(ponto) && tentativas < 50);

        if (tentativas >= 50)
        {
            Debug.LogWarning("Não conseguiu achar ponto dentro da quadra depois de 50 tentativas.");
        }

        return ponto;
    }

    public void MeiaColetada()
    {
        temMeia = true;

        // Para a corotina de desaparecer se a meia foi coletada
        if (desaparecerCoroutine != null)
        {
            StopCoroutine(desaparecerCoroutine);
            desaparecerCoroutine = null;
        }

        meiaAtual = null;
    }

    public void LatinhaColetada()
    {
        temMeia = false;
        latinhaAtual = null;
    }
}