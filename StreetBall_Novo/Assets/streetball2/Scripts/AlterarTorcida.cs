using UnityEngine;
using System.Collections.Generic;

public class AlternarTorcida : MonoBehaviour
{
    [Header("Configurações")]
    public List<Animator> animatorsTorcida; // Lista de Animators
    public float tempoEntreTroca = 5f;

    private float tempoUltimaTroca;
    private bool estaNaPrimeiraAnimacao = true;

    void Start()
    {
        tempoUltimaTroca = Time.time;
    }

    void Update()
    {
        if (Time.time - tempoUltimaTroca >= tempoEntreTroca)
        {
            TrocarAnimacaoDeTodos();
            tempoUltimaTroca = Time.time;
        }
    }

    private void TrocarAnimacaoDeTodos()
    {
        estaNaPrimeiraAnimacao = !estaNaPrimeiraAnimacao;

        foreach (Animator animator in animatorsTorcida)
        {
            if (animator != null)
            {
                animator.SetTrigger("trocarTorcida");
                // Se precisar forçar uma animação específica:
                // animator.Play(estaNaPrimeiraAnimacao ? "Anim1" : "Anim2");
            }
        }
    }
}