using UnityEngine;

public class SpriteToggle : MonoBehaviour
{
    public GameObject imagem1;
    public GameObject imagem2;
    public GameObject imagem3; // Adicionamos a terceira imagem

    public float intervalo = 4f; // Tempo entre cada troca

    private int imagemAtual = 1; // 1 = imagem1, 2 = imagem2, 3 = imagem3

    void Start()
    {
        // Garante que só a primeira imagem está ativa no início
        AtualizarEstado();
        // Inicia o ciclo de troca
        InvokeRepeating(nameof(TrocarImagem), intervalo, intervalo);
    }

    void TrocarImagem()
    {
        imagemAtual++; // Avança para a próxima imagem
        if (imagemAtual > 3) // Se passar de 3, volta para 1
            imagemAtual = 1;
        AtualizarEstado();
    }

    void AtualizarEstado()
    {
        // Ativa apenas a imagem atual e desativa as outras
        if (imagem1 != null) imagem1.SetActive(imagemAtual == 1);
        if (imagem2 != null) imagem2.SetActive(imagemAtual == 2);
        if (imagem3 != null) imagem3.SetActive(imagemAtual == 3);
    }

    void OnDisable()
    {
        // Para o ciclo quando o objeto é desativado
        CancelInvoke(nameof(TrocarImagem));
    }
}