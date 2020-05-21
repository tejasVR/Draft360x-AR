using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Draft360
{
    public class FrameManager : MonoBehaviour
    {
        public static FrameManager Instance;

        [SerializeField] Slider frameSlider;
        [SerializeField] TextMeshProUGUI frameCounterText;

        private List<GameObject> frames = new List<GameObject>();
        private int currentFrameInt;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            CreateNewFrame();
        }

        public void CreateNewFrame()
        {
            var frame = new GameObject("Frame " + frames.Count);
            frame.transform.parent = transform;

            frames.Add(frame);

            if (frames.Count > 1)
                currentFrameInt++;

            frameSlider.maxValue = frames.Count-1;
            frameSlider.value = currentFrameInt;

            ShowFrame(currentFrameInt);
        }

        public GameObject GetCurrentFrame()
        {
            return frames[currentFrameInt];
        }

        public void AddToCurrentFrame(GameObject _gameObjectToAdd)
        {
            _gameObjectToAdd.transform.parent = GetCurrentFrame().transform;
        }

        public void NextFrame()
        {
            if (currentFrameInt + 1 >= frames.Count)
                return;

            currentFrameInt++;
            ShowFrame(currentFrameInt);
        }

        public void PreviousFrame()
        {
            if (currentFrameInt - 1 < 0)
                return;

            currentFrameInt--;
            ShowFrame(currentFrameInt);
        }

        public void SliderValueChanged(Slider _slider)
        {
            ShowFrame((int)_slider.value);
        }

        private void ShowFrame(int _frameToShow)
        {
            HideAllFrames();

            frames[_frameToShow].SetActive(true);

            currentFrameInt = _frameToShow;

            frameCounterText.text = "Frame: " + (currentFrameInt + 1) + "/" + frames.Count;
        }

        private void HideAllFrames()
        {
            foreach (var frame in frames)
            {
                frame.SetActive(false);
            }
        }
    }
}