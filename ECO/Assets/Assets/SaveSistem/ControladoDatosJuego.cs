using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControladoDatosJuego : MonoBehaviour
{
    public GameObject player;

    public string archivoDeGuardado;

    public DatosJuego datosJuego = new();

    private bool primeraCarga = false;
    private int cont = 0;

    private void Start()
    {
        CheckPoint.saveInput += GuardarDatos;

    }
    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
        player = GameObject.FindGameObjectWithTag("Player");
        //CargarDatos();

    }

    private void Update()
    {
        if (!primeraCarga && cont<=5)
        {
            CargarDatos();
            cont++;
        }
        if (cont > 5)
        {
            primeraCarga = true;

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }
    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {

            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            Debug.Log("Posicion Jugador : " + datosJuego.playerPos);
            player.transform.position = datosJuego.playerPos;
            player.GetComponent<PlayerStats>().hp = datosJuego.playerHp;
        }
        else
        {
            Debug.Log("File doesn't exist");
        }
    }
    private void GuardarDatos()
    {

        DatosJuego nuevosDatos = new()
        {
            playerPos = player.transform.position,
            playerHp = player.GetComponent<PlayerStats>().hp

        };
        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("File Saved");

    }
}
