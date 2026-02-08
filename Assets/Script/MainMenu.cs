using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// UnityEngine: biblioteca básica com tudo que o Unity usa (GameObject, MonoBehaviour, etc).
// UnityEngine.SceneManagement: permite carregar e trocar cenas (SceneManager.LoadScene).

public class MainMenu : MonoBehaviour
{
    // Carrega a cena do jogo
    public void PlayGame()
    {
        // Quando o jogador clica em "Jogar", a cena "Jogo" é carregada.
        // O nome deve ser exatamente igual ao que está no Build Settings.
        SceneManager.LoadScene("Jogo");
    }

    // Carrega a cena de créditos
    public void Credits()
    {
        // Quando o jogador clica em "Créditos", a cena "Credito" é carregada.
        // O nome deve ser exatamente igual ao que está no Build Settings.
        SceneManager.LoadScene("Creditos");
    }

    // Volta para o menu principal
    public void ReturnToMainMenu()
    {
        // O nome "Menu" deve ser exatamente o nome da cena no Build Settings.
        SceneManager.LoadScene("Menu");
    }
    public void Tutorial()
    {
        // O nome "Menu" deve ser exatamente o nome da cena no Build Settings.
        SceneManager.LoadScene("Tutorial");
    }

    // Fecha o jogo
    public void QuitGame()
    {
        // Se estiver rodando no Editor, para a simulação
        // Se estiver em um jogo compilado, fecha o aplicativo
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

