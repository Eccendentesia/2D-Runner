using UnityEngine;

public class Destroyer : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("coins") || collision.CompareTag("Enemy") || collision.CompareTag("Magnet") || collision.CompareTag("Shield") || collision.CompareTag("ScoreDoubler") || collision.CompareTag("Support") )
        {
            Destroy(collision.gameObject);
            Debug.Log("Object Destroyed successfully");
        }
    }
}
