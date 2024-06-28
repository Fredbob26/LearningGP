using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public GhostDriver ghostDriver;
    // public float bestLapTime = float.MaxValue;
    // public Transform finishLine; // Объект финишной линии
    // private float currentLapTime;
    //
    // void Start()
    // {
    //     // Убедитесь, что GhostDriver неактивен в начале
    //     if (ghostDriver != null)
    //     {
    //         ghostDriver.gameObject.SetActive(false);
    //     }
    //     else
    //     {
    //         Debug.LogError("GhostDriver не назначен в инспекторе!");
    //     }
    // }
    //
    // void Update()
    // {
    //     // Обновление текущего времени круга
    //     currentLapTime += Time.deltaTime;
    // }
    //
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.transform == finishLine)
    //     {
    //         Debug.Log("Финишная линия пересечена");
    //
    //         // Проверка завершения круга
    //         if (currentLapTime < bestLapTime)
    //         {
    //             bestLapTime = currentLapTime;
    //             Debug.Log("Новый лучший круг: " + bestLapTime);
    //             ghostDriver.StopRecording();
    //             ghostDriver.SaveBestFrames();
    //             ghostDriver.StartRecording();
    //         }
    //         currentLapTime = 0f;
    //
    //         // Активируем GhostDriver и запускаем воспроизведение
    //         ghostDriver.gameObject.SetActive(true);
    //         ghostDriver.StartPlaying();
    //     }
    // }
}
