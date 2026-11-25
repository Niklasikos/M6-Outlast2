using UnityEngine;

public class Grass : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.up*Mathf.Sin(Time.time) * 0.1f;
    }
}
