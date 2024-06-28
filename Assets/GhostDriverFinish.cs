using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDriverFinish : MonoBehaviour
{
    [Header("Ghost Driver")]
    [Tooltip("The GhostDriver that will replay the best lap")]
    public Transform kartTransform; // Ссылка на трансформ картинга-призрака
    public Transform player; // Ссылка на трансформ игрока
    public float recordInterval = 0.1f; // Интервал записи данных в секундах
    public float bestLapTime = 0f; // Лучшее время круга
    
    TimeManager m_TimeManager;
    private bool isStart = true;

    private List<GhostFrame> currentFrames = new List<GhostFrame>();
    private List<GhostFrame> bestFrames = new List<GhostFrame>();
    private bool isRecording = false;
    private bool isPlaying = false;
    private float recordTimer = 0f;
    private float playingTimer = 0f;
    private int playFrameIndex = 0;

    private void Start()
    {
        m_TimeManager = FindObjectOfType<TimeManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isStart)
            {
                StartRecording();
                isStart = false;
                return;
            }
            
            Debug.Log("Финишная линия пересечена");

            float currentLapTime = m_TimeManager.IsFinite ? m_TimeManager.TotalTime - m_TimeManager.TimeRemaining : m_TimeManager.TotalTime;

            // Проверка завершения круга
            if (bestLapTime == 0 || currentLapTime < bestLapTime)
            {
                bestLapTime = currentLapTime;
                Debug.Log("Новый лучший круг: " + bestLapTime);
                StopRecording();
                SaveBestFrames();
                StartRecording();
            }

            // Активируем GhostDriver и запускаем воспроизведение
            kartTransform.gameObject.SetActive(true);
            StartPlaying();
        }
    }

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
            playingTimer += Time.deltaTime;
            if(playingTimer >= recordInterval)
            {
                PlayFrame();
                playingTimer = 0;
            }
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
        currentFrames.Add(new GhostFrame(player.position, player.rotation));
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
