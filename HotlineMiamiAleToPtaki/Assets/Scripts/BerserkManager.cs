using UnityEngine;
 using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class BerserkManager : MonoBehaviour
{
    public static BerserkManager Instance;
    
    [SerializeField] private Slider killCounterSlider;

    private Queue<float> killTimestamps = new Queue<float>(); // Kolejka przechowująca czas zabójstw
    private float killResetTime = 10f; // Czas trzymania zabójstw na pasku
    private int maxKills = 2;
    private float killPower = 25f; // ile berserk powera daje jedno zabójstwo

    private float berserkPower = 0;

    private bool alreadyBerserk = false;

    private playerMovement playerMovement;
    private PlayerPickup playerPickup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Berserk manager set");
        }
        else
        {
            Debug.LogError("BerserkManager already exists - it's assigned to player so something probably went wrong");
        }
    }

    void Start()
    {
        killCounterSlider.maxValue = maxKills * killPower;
        killCounterSlider.value = 0;

        playerMovement = GetComponentInParent<playerMovement>();
        playerPickup = GetComponentInParent<PlayerPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alreadyBerserk){
            DrainBerserkPower();
        }
        else{
            while (killTimestamps.Count > 0 && Time.time - killTimestamps.First() > killResetTime)
            {
                killTimestamps.Dequeue();
            }
            berserkPower = killTimestamps.Count * killPower;

            if (berserkPower >= maxKills * killPower)
            {
                ActivateBerserk();
            }
        }
        
        
        killCounterSlider.value = berserkPower;
    }

    public void RegisterKill(){

        if (killTimestamps.Count < maxKills) 
        {
            killTimestamps.Enqueue(Time.time);
        }
        berserkPower = Mathf.Min(berserkPower + killPower, maxKills * killPower);
    }

    private void ActivateBerserk(){
        alreadyBerserk = true;
        Debug.Log("Berserk activated");
        playerMovement.StartBerserk();
        playerPickup.StartBerserk();
    }

    private void EndBerserk(){
        killTimestamps.Clear();
        alreadyBerserk = false;
        Debug.Log("Berserk mode ended.");
        playerMovement.EndBerserk();
        playerPickup.EndBerserk();
    }

    private void DrainBerserkPower()
    {
        berserkPower -= 10f * Time.deltaTime;
        if (berserkPower <= 0)
        {
            berserkPower = 0;
            EndBerserk();
        }
    }
}
