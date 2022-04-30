using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public int score;
    public float speed;
    public bool needToMove = false;
    public GameController Level;
    public Material[] cubeMaterials;

    private void Update()
    {
        if(needToMove) transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 10f), speed * Time.deltaTime);
    }

    public void changeMaterial()
    {
        GetComponent<MeshRenderer>().material = cubeMaterials[(int)Mathf.Log(score, 2) - 1];
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (collider.GetComponent<CubeScript>().score == score)
            {
                collider.GetComponent<CubeScript>().score += score;
                collider.GetComponent<CubeScript>().changeMaterial();
                Level.totalScore++;
                Destroy(gameObject);
            }
            needToMove = false;
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
