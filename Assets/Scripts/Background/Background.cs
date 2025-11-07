using UnityEngine;

public class Background : MonoBehaviour
{
    private float startPosition, length = 26.9952f;
    public GameObject camera;
    //Speed at which the background should move relative to the camera
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position.x;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Calculate distance background move based on camera movement
        float distance = camera.transform.position.x * parallaxEffect; // 0 = move with camera || 1 = won't move || 0.5 half
        float movement = camera.transform.position.x * (1 - parallaxEffect);  

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        //If background has reached the end of its length adjust its position for infinite scrolling
        if(movement > startPosition +  length)
        {
            startPosition += length;
        }
        else if(movement < startPosition - length)
        {
            startPosition -= length;
        }

    }
}
