﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TinderImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public Image internalImage;
    [HideInInspector]
 //   public Sprite spriteToShow;
    //[HideInInspector]
    public bool internalAnswer;
    [HideInInspector]
    public bool selectedAnswer;
    [HideInInspector]
    public float answerAxis;
    [HideInInspector]
    public string internalText;

    //Private Variables
    private float _yAxis;
    private Vector3 _originalPos;
    private TinderGame _gameCtrl;



    //Event Variables
    public delegate void ValidateAction();
    public static event ValidateAction ValidateImage;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _originalPos = transform.position;
        _yAxis = _originalPos.y;
        internalImage = GetComponent<Image>();
        answerAxis = 0;
//        internalImage.sprite = spriteToShow;
        _gameCtrl = GameObject.FindObjectOfType<TinderGame>();
    }

    public void UpdateSprite(Sprite newSprite)
    {
        internalImage.sprite = newSprite;
    }
    #endregion


    #region INTERFACE_METHODS
    public void OnBeginDrag(PointerEventData eventData)
    {

        internalImage.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        float tempX = Input.mousePosition.x;
        transform.position = new Vector3(Input.mousePosition.x, _yAxis - (Mathf.Abs(transform.localPosition.x) / 10), 0);
        transform.eulerAngles = new Vector3(0, 0, (-transform.localPosition.x / 25));
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        answerAxis = transform.localPosition.x;
        if (answerAxis > 250)
        {
            _gameCtrl.selectedAnswer = true;
            ValidateImage();
        }
        else if (answerAxis < -250)
        {
            _gameCtrl.selectedAnswer = false;
            ValidateImage();
        }
        else
        {
            Debug.Log("Image it wasn't moved");
        }
        internalImage.raycastTarget = true;
        transform.position = _originalPos;
        transform.eulerAngles = new Vector3(0, 0, 0);

    }
    #endregion
}
