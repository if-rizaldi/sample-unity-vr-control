using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.Video;
using System.Linq;


public class HallSceneController : MonoBehaviour
{
    [SerializeField] private bool isScreenUsed;
    [SerializeField] private VolumeProfile volume;
    [SerializeField] private GameObject lampOnObj;
    [SerializeField] private GameObject lampOffObj;
    [SerializeField] private GameObject screenObj;
    [SerializeField] private GameObject spotlight;
    [SerializeField] private GameObject dancingRobot;
    [SerializeField] private Transform dancingPosition;
    [SerializeField] private string videoURL;



    private Animator screenAnim;

    private ShadowsMidtonesHighlights darkEffects;

    bool isLampOn;
    bool isDance;
    bool isSpotlightOn;

    float videoTimeDelay;

    GameObject videoScreen;
    GameObject robotDancerInstantated;

    VideoPlayer videoDemo;


    void Awake()
    {

        foreach (VolumeComponent component in volume.components)
        {
            if (component is ShadowsMidtonesHighlights)
            {
                darkEffects = (ShadowsMidtonesHighlights)component;
            }
        }

        videoScreen = screenObj.transform.GetChild(2).gameObject;

        videoDemo = videoScreen.GetComponent<VideoPlayer>();

        screenAnim = screenObj.GetComponent<Animator>();

    }

    void Start()
    {
        Application.targetFrameRate = 60;

        videoDemo.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoURL.ToString());
        videoDemo.time = 0f;
        videoDemo.controlledAudioTrackCount = 1; 
        //Debug.Log(Application.streamingAssetsPath + "Introducing Microsoft 365 Copilot - Your Copilot for Work 480" );
        //set lamp
        SwitchOn();

        //set video screen
        SwictScreen();
        videoDemo.Stop();
        videoScreen.SetActive(false);

        isDance = true;
        PopUpDancer();

        isSpotlightOn = true;
        SpotlightSwitch();

    }

    public void SwitchLamp()
    {
        if (isLampOn)
            SwitchOff();
        else
            SwitchOn();
    }


    public void SwictScreen()
    {
        if (isScreenUsed)
        {
            screenAnim.SetBool("isUsed", false);
            isScreenUsed = false;
            videoDemo.Stop();
            videoScreen.SetActive(false);
            StopCoroutine(PlayVideoDemo());
        }
        else
        {
            screenAnim.SetBool("isUsed", true);
            isScreenUsed = true;
            StartCoroutine(PlayVideoDemo());
        }

    }

    public void PopUpDancer()
    {
        if (!isDance)
        {
            robotDancerInstantated = Instantiate(dancingRobot, dancingPosition.position, dancingRobot.transform.rotation);
            isDance = true;
        }
        else
        {
            Destroy(robotDancerInstantated);
            isDance = false;

        }

    }

    public void SpotlightSwitch()
    {
        if (!isSpotlightOn)
        {
            spotlight.SetActive(true);
            isSpotlightOn = true;
        }
        else
        {
            spotlight.SetActive(false);
            isSpotlightOn = false;
        }
    }

    public void ExitApplicaiton()
    {
        Application.Quit();
    }


    IEnumerator PlayVideoDemo()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (screenAnim.GetCurrentAnimatorStateInfo(0).IsName("screen-opened") && isScreenUsed == true)
            {
                videoScreen.SetActive(true);
                videoDemo.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoURL.ToString());
                videoDemo.time = 0f; // Set starting time to 0 (zero)
                videoDemo.Prepare(); // Prepare the video player to ensure that it's ready to play
                yield return new WaitUntil(() => videoDemo.isPrepared); // Wait until the video is prepared
                videoDemo.Play();
                

                yield break;

            }


            yield return new WaitForSeconds(0.25f);


        }

    }


    void SwitchOn()
    {
        darkEffects.active = false;
        isLampOn = true;
        lampOffObj.SetActive(false);
        lampOnObj.SetActive(true);


    }

    void SwitchOff()
    {
        isLampOn = false;
        darkEffects.active = true;
        lampOffObj.SetActive(true);
        lampOnObj.SetActive(false);


    }



}
