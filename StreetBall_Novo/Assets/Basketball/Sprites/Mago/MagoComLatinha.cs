using UnityEngine;
using System.Collections;

public class MagoComLatinha : MonoBehaviour
{
    [Header("Configurações")]
    public float tempoSubidaDescida = 1.5f;
    public float alturaSubida = 2f;
    public float intervaloLatinhas = 15f;

    [Header("Referências")]
    public Transform magoBase;
    public Transform magoVisual;
    public Transform handSpawnPoint;
    public ParticleSystem efeitoLancamento;

    [Header("Lançamento")]
    public float duracaoArco = 1.2f;
    public float alturaArco = 2f;
    public float velocidadeRotacao = 360f;

    private Camera cam;
    private float yBase;

    void Awake()
    {
        cam = Camera.main;
        yBase = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 1.5f;
        magoBase.position = new Vector3(cam.transform.position.x, yBase, 0);
        magoBase.SetParent(cam.transform, true);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            yield return StartCoroutine(CicloCompleto());
            yield return new WaitForSeconds(intervaloLatinhas);
        }
    }

    IEnumerator CicloCompleto()
    {
        // Subida
        float t = 0f;
        while (t < tempoSubidaDescida)
        {
            t += Time.deltaTime;
            float progresso = Mathf.Clamp01(t / tempoSubidaDescida);
            magoVisual.localPosition = new Vector3(0, Mathf.Lerp(0, alturaSubida, progresso), 0);
            yield return null;
        }

        // Lançamento
        efeitoLancamento.Play();
        LancarLatinha();
        yield return new WaitForSeconds(duracaoArco);

        // Descida
        t = 0f;
        while (t < tempoSubidaDescida)
        {
            t += Time.deltaTime;
            float progresso = Mathf.Clamp01(t / tempoSubidaDescida);
            magoVisual.localPosition = new Vector3(0, Mathf.Lerp(alturaSubida, 0, progresso), 0);
            yield return null;
        }
    }

    void LancarLatinha()
    {
        if (SpawnSystem.Instance == null) return;

        GameObject latinhaPrefab = SpawnSystem.Instance.temMeia ?
            SpawnSystem.Instance.latinhaEspecialPrefab :
            Random.Range(0, 2) == 0 ?
                SpawnSystem.Instance.latinhaNormalPrefab :
                SpawnSystem.Instance.latinhaNormal2Prefab;

        if (latinhaPrefab == null) return;

        GameObject latinha = Instantiate(latinhaPrefab, handSpawnPoint.position, Quaternion.identity);
        StartCoroutine(MoverLatinha(latinha));
    }

    IEnumerator MoverLatinha(GameObject latinha)
    {
        if (latinha == null || SpawnSystem.Instance == null) yield break;

        Vector3 origem = latinha.transform.position;
        Vector3 destino = SpawnSystem.Instance.GetPosicaoAleatoriaNaQuadra();

        float tempo = 0f;
        while (tempo < duracaoArco && latinha != null)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracaoArco;
            Vector3 pos = Vector3.Lerp(origem, destino, t);
            pos.y += alturaArco * Mathf.Sin(t * Mathf.PI);

            if (latinha != null)
            {
                latinha.transform.position = pos;
                latinha.transform.Rotate(Vector3.forward, velocidadeRotacao * Time.deltaTime);
            }
            yield return null;
        }

        if (latinha != null && SpawnSystem.Instance.temMeia)
        {
            SpawnSystem.Instance.LatinhaColetada();
        }
    }
}