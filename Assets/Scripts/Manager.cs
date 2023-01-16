using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public InputField betInput;
    public Text multiplierText;
    public Text youLostText;
    public Text youWonText;

    public Button cashOutButton;

    public GameObject bomb;
    public GameObject particleEffect;
    public GameObject youLostPanel;
    public GameObject youWonPanel;

    private float betAmount;
    private float multiplier;
    private bool gameStarted;
    private bool bombExploded;
    private readonly bool cashedOut;
    private bool isWon;

    void Start()
    {
        //Inicializar variables
        gameStarted = false;
        bombExploded = false;
        multiplier = 1f;
        cashOutButton.interactable = false;
        youLostPanel.SetActive(false);
        youLostText.enabled = false;
        youWonPanel.SetActive(false);
        youWonText.enabled = false;

        //Asignar evento al InputField
        betInput.onEndEdit.AddListener(StartGame);
    }

    void Update()
    {
        if (gameStarted && !bombExploded && !cashedOut)
        {
            //Aumentar el multiplicador cada segundo
            multiplier += 0.2f * Time.deltaTime;
            multiplierText.text = "x" + multiplier.ToString("F2");

            //Verificar si la bomba estalló
            if (Random.Range(0, 500) == 1)//El rango está en 500 porque suele explotar bastante rápido si lo dejo en 100
            {
                BombExploded();
            }
        }
    }

    void StartGame(string input)
    {
        //Validar y guardar el valor de la apuesta
        if (float.TryParse(input, out betAmount))
        {
            gameStarted = true;
            cashOutButton.interactable = true;
            bomb.SetActive(true);
        }
    }

    public void CashOut()
    {
        //Retirar la apuesta y mostrar el resultado
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
        float winnings = betAmount * multiplier;
        youWonText.text = "You won: $" + winnings;

        //Desactivar botón y activar el causante de fin de juego
        cashOutButton.interactable = false;
        isWon = true;
    }

    void BombExploded()
    {
        //Mostrar animación de explosión
        FindObjectOfType<AudioManager>().Play("Bomb");
        Instantiate(particleEffect, bomb.transform.position, Quaternion.identity);

        //Mostrar mensaje de pérdida o ganancia dependiendo del caso
        if (!isWon)
        {
            StartCoroutine(YouLostPanel());
            youLostText.text = "You lost: $" + betAmount;
        }
        else
        {
            StartCoroutine(YouWonPanel());
        }

        //Desactivar botón y mostrar mensaje de fin de juego
        cashOutButton.interactable = false;
        bombExploded = true;
        gameStarted = false;
        bomb.SetActive(false);
    }

    public void RestartGame()
    {
        //Reinicio del juego
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
        //Ida al menu
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
        SceneManager.LoadScene("Menu");
    }

    IEnumerator YouLostPanel()
    {
        //Activar panel de perdiste
        yield return new WaitForSeconds(3);
        FindObjectOfType<AudioManager>().Play("YouLost");
        youLostPanel.SetActive(true);
        youLostText.enabled = true;
    }

    IEnumerator YouWonPanel()
    {
        //Activar panel de ganaste

        yield return new WaitForSeconds(3);
        FindObjectOfType<AudioManager>().Play("YouWon");
        youWonPanel.SetActive(true);
        youWonText.enabled = true;
    }
}
