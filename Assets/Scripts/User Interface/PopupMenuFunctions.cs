using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenuFunctions : MonoBehaviour
{
    private GameObject cursor;
    private RectTransform trans;
    private Image image;
    private bool visibility;
    void Start()
    {
        visibility = false;
        trans = gameObject.GetComponent<RectTransform>();
        cursor = GameObject.Find("Cursor");
        image = gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Empty");



    }

    // Update is called once per frame
    void Update()
    {
        
        updatePosition();

    }






    public void updatePosition()
    {
        if (Input.mousePosition.x > Screen.width / 2)
        {
            trans.anchoredPosition = new Vector2(Screen.width / 2 - 100, 0);

        }
        else
        {
            trans.anchoredPosition = new Vector2(-Screen.width / 2 + 100, 0);
        }
    }
    public Image getImage()
    {
        return image;
    }

    public bool getVisibility()
    {
        return visibility;
    }

    public void setVisibility(bool state)
    {
        visibility = state;
    }


    public void updateVisibility()
    {
        if (visibility == false)
        {
            image.sprite = Resources.Load<Sprite>("Empty");
            gameObject.GetComponent<GenerateButtons>().hideButtons();
        }
        else if (visibility == true)
        {
            image.sprite = Resources.Load<Sprite>("MenuAsset");
            gameObject.GetComponent<GenerateButtons>().showButtons();
        }
    }


}
