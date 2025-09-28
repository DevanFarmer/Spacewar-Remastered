using EventBusEventData;
using System.Collections;
using UnityEngine;

public class BossMovementUtilities : MonoBehaviour
{
    void MoveToDeathLocation(Vector2 deathLocation, float speed)
    {
        StartCoroutine(MoveToLocation(deathLocation, speed));
    }

    IEnumerator MoveToLocation(Vector2 location, float speed)
    {
        while ((Vector2)transform.position != location)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                location,
                speed * Time.deltaTime);

            yield return null;
        }

        EventBus.Publish(new OnBossDeathLocationReached());
    }
}
