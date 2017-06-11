using UnityEngine;

public class PowerUp : MonoBehaviour
{
    PowerUpManager powerUpManager;

    void Start()
    {
        powerUpManager = (PowerUpManager)FindObjectOfType(typeof(PowerUpManager));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Cat")
            Activate((Character)col.gameObject.GetComponent(typeof(Character)));
    }

    protected virtual void Activate(Character enemy) { }

    protected void Destroy()
    {
        powerUpManager.Remove(gameObject);
        Destroy(gameObject);
    }
}
