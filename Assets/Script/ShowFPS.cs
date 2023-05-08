using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;


    void Start()
    {
        StartCoroutine(FramesPerSecond());
    }

    
    private IEnumerator FramesPerSecond()
    {
        while (true)
        {
            int fps = (int)(1f / Time.deltaTime);
            DisplayFPS(fps);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void DisplayFPS(float fps)
    {
        fpsText.text = $"{fps} FPS";
    }

}
