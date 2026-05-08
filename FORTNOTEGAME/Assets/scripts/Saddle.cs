using UnityEngine;

public class Saddle : MonoBehaviour
{
    [SerializeField]
    GameObject Cat;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Cat.transform.position.x, Cat.transform.position.y+2, Cat.transform.position.z);
    }
}
