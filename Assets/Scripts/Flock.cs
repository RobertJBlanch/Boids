using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Flock : MonoBehaviour
{
    public List<Bird> flock = new List<Bird>();
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    private Settings settings;

    void Awake(){
        settings = Settings.instance;
    }
    void Update()
    {
        foreach (Bird bird in flock)
        {
            // Vector3 v = Cohesion(bird) + Alignment(bird) + Seperation(bird);
            Vector3 v = Vector3.zero;
            if(settings.seperation)
                v += Seperation(bird) * settings.seperationFactor;
            if(settings.alignment)
                v += Alignment(bird) * settings.alignmentFactor;
            if(settings.cohesion)
                v += Cohesion(bird) * settings.cohesionFactor;
            if(settings.stayInRadius)
                v += StayInRadius(bird) * settings.stayInRadiusFactor;
            v = v.normalized; 
            bird.Move(v);
        }
    }

    Vector3 StayInRadius(Bird bird){
        Vector2 centerOffset = settings.center - (Vector2)bird.transform.position;
        float t = centerOffset.magnitude / settings.radius;
        if(t < 0.9f){
            return Vector2.zero;
        }
        return centerOffset * t * t;
    }

    Vector3 Cohesion(Bird bird){

        List<Bird> birdsWithinDist = GetBirdsWithinDistance(bird, settings.neighbourRadius);

        if(birdsWithinDist.Count == 0)
            return Vector3.zero;

        Vector3 cohensionMove = new Vector3(0, 0, 0);

        foreach (Bird birdWithinDist in birdsWithinDist)
        {
            cohensionMove += birdWithinDist.transform.position;
        }

        cohensionMove /= birdsWithinDist.Count;

        cohensionMove -= bird.transform.position;
        cohensionMove = Vector2.SmoothDamp(bird.transform.up, cohensionMove, ref currentVelocity, agentSmoothTime);

        return cohensionMove;
    }

    Vector3 Alignment(Bird bird){

        List<Bird> birdsWithinDist = GetBirdsWithinDistance(bird, settings.neighbourRadius);

        if(birdsWithinDist.Count == 0)
            return Vector3.zero;

        Vector3 avgHeading = new Vector3(0, 0, 0);

        foreach (Bird birdWithinDist in birdsWithinDist)
        {
            avgHeading += birdWithinDist.transform.up;
        }

        avgHeading = avgHeading / birdsWithinDist.Count;

        return avgHeading;
    }

    Vector3 Seperation(Bird bird){

        List<Bird> birdsWithinDist = GetBirdsWithinDistance(bird, settings.neighbourRadius * settings.seperationRadiusFactor);

        if(birdsWithinDist.Count == 0)
            return Vector3.zero;

        Vector3 pos = new Vector3(0, 0, 0);

        foreach (Bird birdWithinDist in birdsWithinDist)
        {
            pos += bird.transform.position - birdWithinDist.transform.position;
        }

        pos = pos / birdsWithinDist.Count;

        // Vector3 dir = (bird.transform.position - pos);

        return pos;
    }

    List<Bird> GetBirdsWithinDistance(Bird bird, float distance){

        List<Bird> birdsWithinDist = GetOtherBirdsInFlock(bird);
        birdsWithinDist = birdsWithinDist.Where( x => Vector3.Distance(x.transform.position, bird.transform.position) <= distance).ToList();

        return birdsWithinDist;
    }

    List<Bird> GetOtherBirdsInFlock(Bird bird){
        List<Bird> temporary = new List<Bird>(flock);
        temporary.Remove(bird);
        return temporary;
    }
}
