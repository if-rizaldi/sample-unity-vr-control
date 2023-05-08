using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ButtonUI : MonoBehaviour
{

    [SerializeField] private Color buttonNormalColor;
    [SerializeField] private Color textColorNormal;
    [SerializeField] private Color buttonSelectedColor;
    [SerializeField] private Color textColorSelected;
    [SerializeField] private Color buttonHighlightedColor;
    Image buttonImage;
    TextMeshProUGUI buttonText;

    bool isSelected;

    public bool isHighlighted;

    Color buttonLastColor;
    Color textLastColor;

    IEnumerator checkButtonWorld;


    void Start()
    {
        isSelected = false;
        buttonImage = GetComponent<Image>();
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        buttonImage.color = buttonNormalColor;
        buttonText.color = textColorNormal;
        buttonLastColor = buttonImage.color;
        isHighlighted = true;

        checkButtonWorld = CheckButtonOnWorldTouched();
    }

    public void ButtonSelect()
    {
        if (isSelected)
        {
            buttonImage.color = buttonLastColor;
            buttonText.color = textColorNormal;
            isSelected = false;
        }
        else
        {
            buttonImage.color = buttonSelectedColor;
            buttonText.color = textColorSelected;
            isSelected = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Controller Stick")
        {
            buttonImage.color = buttonHighlightedColor;
            StartCoroutine(checkButtonWorld);
            buttonLastColor = buttonImage.color;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Controller Stick")
        {
            if (!isSelected)
            {
                buttonImage.color = buttonNormalColor;
                buttonLastColor = buttonImage.color;
            }
            else
            {
                buttonImage.color = buttonSelectedColor;

            }

            StopCoroutine(checkButtonWorld);


        }
    }

    IEnumerator CheckButtonOnWorldTouched()
    {
        while (true)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GetComponent<Button>().onClick.Invoke();

            }

            yield return null;
        }

    }


}
