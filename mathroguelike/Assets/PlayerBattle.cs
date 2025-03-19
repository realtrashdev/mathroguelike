using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public class PlayerBattle : MonoBehaviour, IDamageable
{
    [SerializeField] private NumberInfoEventChannel numberChannel;
    
    NumberInfoEvent numberSpawnEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        NumberInfo info = new NumberInfo(transform.position, damage);
        numberSpawnEvent.Info = info;

        //spawn damage number
        numberChannel.CallEvent(numberSpawnEvent);
    }
}
