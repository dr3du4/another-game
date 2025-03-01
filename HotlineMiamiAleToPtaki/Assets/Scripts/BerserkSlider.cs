 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;

 public class BerserkSlider : MonoBehaviour
{
    public Slider killCounterSlider;
    
    private Queue<float> killTimestamps = new Queue<float>(); // Kolejka przechowująca czas zabójstw
    private float killResetTime = 10f; // Czas trzymania zabójstw na pasku
    private int maxKills = 4;

    void Start()
    {
        killCounterSlider.maxValue = maxKills;
        killCounterSlider.value = 0;
    }

    void Update()
    {
      
        while (killTimestamps.Count > 0 && Time.time - killTimestamps.Peek() > killResetTime)
        {
            killTimestamps.Dequeue();
        }
        
        killCounterSlider.value = Mathf.Lerp(killCounterSlider.value, killTimestamps.Count, Time.deltaTime * 5);
    }

  
    public void AddKill()
    {
        if (killTimestamps.Count < maxKills) 
        {
            killTimestamps.Enqueue(Time.time);
        }
    }
}
