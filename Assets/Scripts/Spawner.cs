using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject flockPrefab;
    public GameObject birdPrefab;
    public int flocks;
    public int birds;
    [ColorUsageAttribute(true,true)]
    public Color color1;
    [ColorUsageAttribute(true,true)]
    public Color color2;
    [ColorUsageAttribute(true,true)]
    public Color color3;
    private List<Color> colors = new List<Color>();
    void Start()
    {

        colors.Add(color1);
        colors.Add(color2);
        colors.Add(color3);

        for (int i = 0; i < flocks; i++)
        {
            var pos = Random.insideUnitCircle * 5;
            var flockObj = Instantiate(flockPrefab, pos, flockPrefab.transform.rotation);
            Flock flock = flockObj.GetComponent<Flock>();

            // Color color = new Color(
            //     Random.Range(0, 1f), 
            //     Random.Range(0, 1f), 
            //     Random.Range(0, 1f)
            // );
            
            Color color = colors[i];

            for (int ii = 0; ii < birds; ii++)
            {
                var posBird = Random.insideUnitCircle * 5 + pos;
                var bird = Instantiate(birdPrefab, posBird, birdPrefab.transform.rotation);
                bird.transform.eulerAngles = new Vector3(0f, Random.Range(0f, 360f), Random.Range(0f, 360f));
                bird.transform.GetComponentInChildren<SpriteRenderer>().color = color;
                flock.flock.Add(bird.GetComponent<Bird>());
            }
        }
    }
}
