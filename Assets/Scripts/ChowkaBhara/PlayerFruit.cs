
using UnityEngine;

public class PlayerFruit : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;

    private Renderer objectRenderer;

    [Header("Ripen Variables")]
    [SerializeField] private Vector3 ripen = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private Color ripe;
    [SerializeField] private Color semiRipe;
    [SerializeField] private Color raw;

    [Header("Object Disatance Calculation")]
    [SerializeField] public Transform fruit; //objectA
    [SerializeField] private Vector3 centerCoordinates;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        objectRenderer = GetComponent<Renderer>();
    }

    private void Update() {

        float speed = Vector3.Magnitude(rb.linearVelocity);

        if (speed> 0.2f) {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            else {
                audioSource.Stop();
            }
        }

        ColorControl();
    }

    public void Ripen() {
        gameObject.transform.localScale = gameObject.transform.localScale + ripen;
    }

    float EarthDistance() {
        if (fruit != null)
        {
            // Calculate the distance between the objects
            float distance = Vector3.Distance(fruit.position, centerCoordinates);

            // Log the distance to the console
            Debug.Log("Distance between Object A and Object B: " + distance);
            return distance;
        }

        else {
            return 0f;
        }
    }

    void ColorControl() {
        switch (EarthDistance()) {
            case <0.7f:
                objectRenderer.material.color = ripe;
            break;

            case <1.4f:
                objectRenderer.material.color = semiRipe;
            break;

            case >1.4f:
            objectRenderer.material.color = raw;
            break;
        }
    }


}
