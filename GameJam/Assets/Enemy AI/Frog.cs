using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public enum FrogState { SpawningFlies, Croaking, EatingFly,Idle}
    [SerializeField]private FrogState State = FrogState.SpawningFlies;
    [SerializeField]private GameObject flyPrefab;
    [SerializeField] private float xPos, yPos;
    [SerializeField] private int flyCount, maxFlies = 5;
    [SerializeField] private float PauseTime = 50f, TimeBetweenFlySpawns = .5f, CroakTime = 3, SpitLiveTime = 2f;
    [SerializeField]private List<GameObject> flies = new List<GameObject>();
    [SerializeField] Vector2 flySpawnMinXY, flySpawnMaxXY;
    [SerializeField] Projectile Spit;
    Coroutine flySpawn, flyEat, croakingAnimation;
    private bool isVulnerable, isCroaking;
    private double health;

    void Start()
    {
        State = FrogState.SpawningFlies;
        StartCoroutine((FlyDrop()));
        StartCoroutine((EatFly()));
    }

    void Update()
    {
        isVulnerable = flies.Count == 0;
        switch(State)
        {
            case FrogState.SpawningFlies:
                if(flySpawn == null)
                {
                    flySpawn = StartCoroutine((FlyDrop()));
                }
                break;
            case FrogState.EatingFly:
                if(flyEat == null)
                {
                    flyEat = StartCoroutine((EatFly()));
                }    
                break;
            case FrogState.Croaking:
                if(!isCroaking)
                {
                    isCroaking = true;
                    croakingAnimation = StartCoroutine(Croak());
                }
                break;
        }
    }

    // sometimes the flies don't show and I don't know why
    // Spawns flies randomly
    IEnumerator FlyDrop() {
        while (flyCount < maxFlies) 
        {
                xPos = Random.Range(-5f, 5); // add 1 to max
                yPos = Random.Range(-5f,5);
            print($"X: {xPos}, Y: {yPos}");
                flies.Add(Instantiate(flyPrefab, new Vector2(transform.position.x + xPos, transform.position.y + yPos), Quaternion.identity));
                flyCount = flies.Count;
                yield return new WaitForSeconds(TimeBetweenFlySpawns); // 1/2 of a second betweeen spawns
        }
        State = FrogState.EatingFly;
        flySpawn = null;
    }

    IEnumerator EatFly() 
    {
        while (flyCount > 0) 
        {
            //eat fly animation
            System.Random rnd = new System.Random();
            int index = rnd.Next(0, flyCount);
            GameObject flyCurrent = flies[index] as GameObject;
            Destroy(flyCurrent);
            flies.RemoveAt(index);
            yield return new WaitForSeconds(1f);
            Destroy(Instantiate(Spit, transform.position, Quaternion.identity).gameObject, SpitLiveTime);
            print("Attack");
            yield return new WaitForSeconds(3f);
            flyCount = flies.Count;
        }
        State = FrogState.Croaking;
        flyEat = null;
    }
    IEnumerator Croak()
    {
        //PlayCroak Sound
        yield return new WaitForSeconds(CroakTime);
        maxFlies += 2;
        State = FrogState.SpawningFlies;
        isCroaking = false;
        croakingAnimation = null;
    }
}
