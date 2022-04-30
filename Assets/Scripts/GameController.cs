using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int totalScore  = 0;
    public float sideSpeed;
    public Vector3 spawnPoint;
    public Material[] cubeMaterials;
    public Text scoreText;
    public GameObject cubePrefab;
    GameObject curCube;

    void Start()
    {
        GenerateCube();
    }

    void Update()
    {
        scoreText.text = "Your Score: " + totalScore;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (curCube)
            {
                curCube.transform.position = Vector3.MoveTowards(curCube.transform.position, new Vector3(Camera.main.ScreenToViewportPoint(touch.position).x * 4, curCube.transform.position.y, curCube.transform.position.z), sideSpeed * Time.deltaTime);
                if (touch.phase == TouchPhase.Ended)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        curCube.GetComponent<CubeScript>().needToMove = true;
        curCube = null;
        yield return new WaitForSeconds(2f);
        GenerateCube();
    }

    private void GenerateCube()
    {
        curCube = Instantiate(cubePrefab, spawnPoint, Quaternion.identity);
        int curScore = Random.Range(1, 4);
        curCube.GetComponent<CubeScript>().score = (int)Mathf.Pow(2, curScore);
        curCube.GetComponent<CubeScript>().Level = this;
        curCube.GetComponent<MeshRenderer>().material = cubeMaterials[curScore - 1];
    }
}
