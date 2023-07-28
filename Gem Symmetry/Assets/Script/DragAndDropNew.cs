using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropNew : MonoBehaviour
{
    private Vector3 gemPos;

    public float angle;

    private Vector3 endPoint;

    public List<GameObject> connections = new List<GameObject>();

    private bool textPlaced = false;

    void Start()
    {
        transform.GetChild(0).transform.RotateAround(transform.position, transform.forward, angle);

        transform.GetChild(1).transform.RotateAround(transform.position, transform.forward, angle/2);

        transform.GetChild(1).GetComponent<TextMeshPro>().text = angle.ToString() + "°";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hitOne = Physics2D.Raycast(transform.position, transform.GetChild(0).right);

        RaycastHit2D hitTwo = Physics2D.Raycast(transform.position, Vector3.Reflect(transform.GetChild(0).right, Vector3.right));

        RaycastHit2D hitThree = Physics2D.Raycast(transform.position, -transform.GetChild(0).right);

        RaycastHit2D hitFour = Physics2D.Raycast(transform.position, -Vector3.Reflect(transform.GetChild(0).right, Vector3.right));

        #region Debugging

        //Debug.Log(gameObject + " is hitting " + hitOne[1].collider.gameObject); //+ " " + hitTwo.collider.gameObject + " " + hitThree.collider.gameObject + " " + hitFour.collider.gameObject);

        Debug.DrawRay(transform.position, transform.GetChild(0).right, Color.red);

        Debug.DrawRay(transform.position, -transform.GetChild(0).right, Color.red);

        Debug.DrawRay(transform.position, Vector3.Reflect(transform.GetChild(0).right, Vector3.right), Color.red);

        Debug.DrawRay(transform.position, -Vector3.Reflect(transform.GetChild(0).right, Vector3.right), Color.red);

        #endregion

        if (hitOne || hitTwo || hitThree || hitFour)
        {
            ConnectionMaker(hitOne);
            ConnectionMaker(hitTwo);
            ConnectionMaker(hitThree);
            ConnectionMaker(hitFour);
        }
        else
        {
            foreach (GameObject connection in connections)
            {
                connection.GetComponent<SpriteRenderer>().color = Color.white;
            }

            transform.GetChild(2).GetComponent<LineRenderer>().enabled = false;

            connections.Clear();

            transform.GetChild(1).gameObject.SetActive(false);

            textPlaced = false;
        }

    }

    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        transform.position = mousePos;

        //gameObject.GetComponent<LineRenderer>().enabled = true;

        transform.GetChild(1).gameObject.SetActive(true);

        //gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        //gameObject.GetComponent<LineRenderer>().SetPosition(1, transform.position + new Vector3(3, 0, 0));

        endPoint += transform.position;

        //gameObject.GetComponent<LineRenderer>().SetPosition(2, transform.position);
        //gameObject.GetComponent<LineRenderer>().SetPosition(3, transform.GetChild(0).position);

    }

    private void OnMouseUp()
    {
        transform.GetChild(1).gameObject.SetActive(false);

        //gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    private void ConnectionMaker(RaycastHit2D x)
    {
        if (x != false)
        {
            if (x.collider.tag == "Gem")
            {
                var a = x.collider;

                Debug.Log("Meow!");

                try
                {
                    if (connections[0] != x.collider.gameObject)
                    {
                        connections.Add(x.collider.gameObject);
                    }
                }
                catch
                {
                    connections.Add(x.collider.gameObject);
                }

                x.collider.GetComponent<SpriteRenderer>().color = Color.red;

                transform.GetChild(2).GetComponent<LineRenderer>().enabled = true;

                transform.GetChild(2).GetComponent<LineRenderer>().SetPosition(0, transform.position);
                transform.GetChild(2).GetComponent<LineRenderer>().SetPosition(1, x.collider.transform.position);

                transform.GetChild(2).GetComponent<LineRenderer>().SetPosition(2, transform.position);
                transform.GetChild(2).GetComponent<LineRenderer>().SetPosition(3, transform.position + new Vector3(3, 0, 0));

                transform.GetChild(1).gameObject.SetActive(true);

                placeText(x.collider.transform);

                
            }
        }

    }

    private void placeText(Transform a)
    {
        transform.GetChild(1).GetComponent<TextMeshPro>().text = (int)Vector3.Angle(new Vector3(transform.position.x + 4, transform.position.y, 0) - transform.position, a.position - transform.position) + "°";

        if (!textPlaced)
        {
            //transform.GetChild(1).Rotate(transform.position, transform.forward, Vector3.Angle(new Vector3(transform.position.x + 2, transform.position.y, 0), transform.position+a.position) / 2);

            textPlaced = true;
        }
    }
}

