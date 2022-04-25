
using UnityEngine;

public class SinkScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 temp;
    public float sinkingSpeed = 0.5f;

    void Start()
    {

        temp = transform.position;
        temp.y = 0;
        GetComponent<BoxCollider>().isTrigger = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.position != temp){
            gameObject.transform.position = Vector3.MoveTowards(transform.position, temp, sinkingSpeed * Time.deltaTime);
        }else {
            Destroy(this.gameObject);
        }
    }
}
