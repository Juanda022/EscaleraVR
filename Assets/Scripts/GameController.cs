using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Transform> bases; // Lista de las bases
    public List<Transform> cubos; // Lista de los cubos

    void Start()
    {
        ActualizarPosiciones(); // Actualizamos las posiciones al inicio del juego
    }

    // Método para actualizar las posiciones de los cubos en las bases
    public void ActualizarPosiciones()
    {
        // Reiniciar la lista de posiciones de cubos
        int[] posiciones = new int[bases.Count];
        for (int i = 0; i < posiciones.Length; i++)
        {
            posiciones[i] = 0; // Inicializamos todas las posiciones como vacías
        }

        for (int i = 0; i < cubos.Count; i++)
        {
            // Encuentra la base más cercana para el cubo actual
            int baseIndex = EncontrarBaseMasCercana(cubos[i]);
            if (baseIndex != -1)
            {
                posiciones[baseIndex] = i + 1; // Guardamos el índice del cubo en la base correspondiente
                cubos[i].position = bases[baseIndex].position; // Mueve el cubo a la base más cercana
            }
        }

        // Imprimir la disposición de los cubos en las bases
        Debug.Log("[" + string.Join(",", posiciones) + "]");
    }

    int EncontrarBaseMasCercana(Transform cubo)
    {
        int baseIndex = -1;
        float distanciaMinima = Mathf.Infinity;

        for (int i = 0; i < bases.Count; i++)
        {
            float distancia = Vector3.Distance(cubo.position, bases[i].position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                baseIndex = i;
            }
        }

        return baseIndex;
    }
}
