using UnityEngine;

public class Grass : MonoBehaviour
{
    private float rotation;
    public float cesped_velocidad = 10;
    public float cesped_fuerza = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var objects = GameObject.FindGameObjectsWithTag("cesped");
        var objectCount = objects.Length;
        foreach (var obj in objects) {
            obj.transform.rotation = Quaternion.Euler(Mathf.Sin((Time.time+obj.transform.position.x*50)*cesped_velocidad)*cesped_fuerza, (transform.position.x+transform.position.z*0.3f)*999, Mathf.Sin((Time.time+obj.transform.position.z*50)*cesped_velocidad*0.75f)*cesped_fuerza);
        }
    }
}
