using System.Collections.Generic;
using UnityEngine;

public class GhostDriver : MonoBehaviour
{
    public Transform kartTransform; // Ссылка на трансформ картинга-призрака
    public float recordInterval = 0.1f; // Интервал записи данных в секундах
    public float bestLapTime = 0f; // Лучшее время круга

    private List<GhostFrame> currentFrames = new List<GhostFrame>();
    private List<GhostFrame> bestFrames = new List<GhostFrame>();
    private bool isRecording = false;
    private bool isPlaying = false;
    private float recordTimer = 0f;
    private int playFrameIndex = 0;

    void Update()
    {
        if (isRecording)
        {
            recordTimer += Time.deltaTime;
            if (recordTimer >= recordInterval)
            {
                RecordFrame();
                recordTimer = 0f;
            }
        }

        if (isPlaying)
        {
            PlayFrame();
        }
    }

    public void StartRecording()
    {
        isRecording = true;
        currentFrames.Clear();
        recordTimer = 0f;
        Debug.Log("Начата запись движений");
    }

    public void StopRecording()
    {
        isRecording = false;
        Debug.Log("Остановлена запись движений");
    }

    public void SaveBestFrames()
    {
        bestFrames = new List<GhostFrame>(currentFrames);
        Debug.Log("Сохранены лучшие кадры");
    }

    public void StartPlaying()
    {
        isPlaying = true;
        playFrameIndex = 0;
        Debug.Log("Начато воспроизведение движений");
    }

    public void StopPlaying()
    {
        isPlaying = false;
        Debug.Log("Остановлено воспроизведение движений");
    }

    private void RecordFrame()
    {
        currentFrames.Add(new GhostFrame(kartTransform.position, kartTransform.rotation));
    }

    private void PlayFrame()
    {
        if (playFrameIndex < bestFrames.Count)
        {
            kartTransform.position = bestFrames[playFrameIndex].position;
            kartTransform.rotation = bestFrames[playFrameIndex].rotation;
            playFrameIndex++;
        }
        else
        {
            StopPlaying();
            Debug.Log("Воспроизведение завершено");
        }
    }
}
