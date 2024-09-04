using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float forceMagnitude = 10f;
    [SerializeField] float attractionDistance = 1f;

    private Rigidbody targetRigidbody;
    private GameController gameController;

    void Start()
    {
        gameController = GetComponentInParent<GameController>(); // Obtenemos el controlador desde el padre
        if (target != null)
        {
            targetRigidbody = target.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = transform.position - target.position;
            float distance = direction.magnitude;

            if (distance < attractionDistance)
            {
                targetRigidbody.isKinematic = true;
                target.position = transform.position;
                gameController.ActualizarPosiciones();  // Notifica al GameController para actualizar posiciones
            }
            else
            {
                targetRigidbody.isKinematic = false;
                targetRigidbody.AddForce(direction.normalized * forceMagnitude);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (target == null && other.CompareTag("Target"))
        {
            target = other.transform;
            targetRigidbody = target.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target") && other.transform == target)
        {
            targetRigidbody.isKinematic = false;
            target = null;
            targetRigidbody = null;
            gameController.ActualizarPosiciones();  // Notifica al GameController para actualizar posiciones cuando un cubo sale
        }
    }
}
