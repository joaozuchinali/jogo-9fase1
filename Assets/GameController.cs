using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    private static int pontos = 0;
    private static int vida = 3;

    [SerializeField]
    private static TMP_Text texto;

    [SerializeField]
    private static TMP_Text vidas;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go1 = GameObject.FindGameObjectWithTag("textopontos");
        texto = go1.GetComponent<TMP_Text>();

        GameObject go2 = GameObject.FindGameObjectWithTag("textovida");
        vidas = go2.GetComponent<TMP_Text>();

        // Salva o nome da cena atual no PlayerPrefs
        string currentLevel = SceneManager.GetActiveScene().name;

        if(currentLevel != "GameOver") {
            PlayerPrefs.SetString("CurrentLevel", currentLevel);
        }
        
        vidas.text = "Vidas: " + vida.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setPontos(int ponto) {
        pontos += ponto;
        texto.text = "Pontos: " + pontos.ToString();
    }

    private static void setVidas() {
        vidas.text = "Vidas: " + vida.ToString();
    }

    public static void setPerda()
    {
        vida--;
        setVidas();

        if(vida <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public static void proximaFase(string nomeFase) 
    {
        pontos = 0;
        SceneManager.LoadScene(nomeFase);
    }

    public static void restart() 
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
    }
}
