using EventBusEventData;
using System.Collections;
using UnityEngine;

public class BossMovementUtilities : MonoBehaviour
{
    public void MoveToLocation<T>(Vector2 deathLocation, float speed, T reachedEvent) where T : struct, EventBusEventData.IReachedLocationEvent
    {
        StartCoroutine(HandleMoveToLocation(deathLocation, speed, reachedEvent));
    }

    IEnumerator HandleMoveToLocation<T>(Vector2 location, float speed, T reachedEvent) where T : struct, EventBusEventData.IReachedLocationEvent
    {
        while ((Vector2)transform.position != location)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                location,
                speed * Time.deltaTime);

            yield return null;
        }

        EventBus.Publish(reachedEvent);
    }
}
