using UnityEngine;

public class SideWarp : MonoBehaviour
{

    private const float shipWidthUnityUnits = 1.01f;
    private const float screenWidthUnityUnits = 18.046f;
    private const float warpDistance = screenWidthUnityUnits + shipWidthUnityUnits;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x && gameObject.CompareTag("LeftSide"))
                collision.transform.position = new Vector3(collision.transform.position.x + warpDistance, collision.transform.position.y);
            else if (collision.transform.position.x > transform.position.x && gameObject.CompareTag("RightSide"))
                collision.transform.position = new Vector3(collision.transform.position.x - warpDistance, collision.transform.position.y);
        }
    }

}
