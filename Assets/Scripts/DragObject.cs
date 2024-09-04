using UnityEngine;

public class DragObject : MonoBehaviour
{
    //SerializeField muestra los datos en el inspector de Unity
    public delegate void DragEndedDelegate(DragObject draggableObject);
    public DragEndedDelegate dragEndedCallback;
    [SerializeField] bool hasPhysics = true;
    [SerializeField] float forceMultipler = 1f;
    [SerializeField] float minDragDistance = 0.6f;
    [SerializeField] Camera cam;

    Vector3 mousePos;
    Vector3 startPos;
    Rigidbody rb;
    Vector3 initialVelocity;

    // Start is called before the first frame update
    void Start(){
    if (cam == null){ cam = Camera.main;}
    if (hasPhysics){ rb = GetComponent<Rigidbody>();}
    }

    Vector3 GetMousePos(){
        return cam.WorldToScreenPoint(transform.position);
    }
    private void OnMouseDown(){
        mousePos = Input.mousePosition - GetMousePos();
        startPos = transform.position;
        if (hasPhysics){
            initialVelocity = rb.velocity;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnMouseDrag(){
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition-mousePos);
    }
    private void OnMouseUp(){ //Cuando pulsamos el click izquierdo
        if (!hasPhysics) return;

        Vector3 dragDirection = transform.position - startPos;
        float dragDistance = dragDirection.magnitude;
        if(dragDistance >= minDragDistance && rb.velocity.magnitude <= 1f){
            dragDirection.Normalize();
            rb.AddForce(dragDirection * dragDistance *forceMultipler, ForceMode.Impulse);
        }
        rb.velocity = initialVelocity;

        dragEndedCallback?.Invoke(this);
    }
}
