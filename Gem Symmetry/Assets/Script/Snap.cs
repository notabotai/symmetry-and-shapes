using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gem")
        {
            Debug.Log(collision.gameObject.transform);

            collision.gameObject.transform.position = gameObject.transform.position;

            if(collision.gameObject.GetComponent<DragAndDropNew>().angle == 90)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }
}
