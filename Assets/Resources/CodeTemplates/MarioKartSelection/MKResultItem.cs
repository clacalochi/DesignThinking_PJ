﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MKResultItem : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    public MKItem[] _MKItemsArray;
    private GameObject[] _displayedObjsArray;
    private Transform _3Dholder;
    public int[] _obtainedResultsArray;
    private int group1, group2, group3;
    #endregion
    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    void OnEnable()
    {
        MKItem.OnClicked += ValidateAnswer;
    }

    void OnDisable()
    {
        MKItem.OnClicked -= ValidateAnswer;
    }
    #endregion
    #region CREATED_METHODS
    private void Initializate()
    {
        _MKItemsArray = FindObjectsOfType<MKItem>();
        _obtainedResultsArray = new int[_MKItemsArray.Length];

        //Order the array
        Array.Sort(_MKItemsArray);
        _3Dholder = transform.GetChild(0);
        _displayedObjsArray = new GameObject[_3Dholder.childCount - 1];
        for (int i = 0; i < _displayedObjsArray.Length; i++)
        {
            _displayedObjsArray[i] = _3Dholder.GetChild(i).gameObject;
            _displayedObjsArray[i].SetActive(false);
        }
        group1 = 0; group2 = 3; group3 = 6;
        UpdateObtainedResults();
    }

    private void ValidateAnswer()
    {
        int temp = 0;
        foreach (MKItem item in _MKItemsArray)
        {
            if (item.isCorrect)
            {
                temp++;
            }
        }
        UpdateObtainedResults();
        if (temp != 0 && temp == _MKItemsArray.Length)
        {
            FinishGame();
        }
    }

    private void UpdateObtainedResults()
    {
        UnselectContent();
        for (int i = 0; i < _MKItemsArray.Length; i++)
        {
            _obtainedResultsArray[i] = _MKItemsArray[i].GetImgCtrl();
        }
        UpdateContent();
    }

    public virtual void UpdateContent()
    {
        /*         foreach (GameObject go in _displayedObjsArray)
                {
                    go.SetActive(false);
                } */
        _displayedObjsArray[_obtainedResultsArray[0] + group1].SetActive(true);
        _displayedObjsArray[_obtainedResultsArray[1] + group2].SetActive(true);
        _displayedObjsArray[_obtainedResultsArray[2] + group3].SetActive(true);
    }
    private void UnselectContent()
    {
        _displayedObjsArray[_obtainedResultsArray[0] + group1].SetActive(false);
        _displayedObjsArray[_obtainedResultsArray[1] + group2].SetActive(false);
        _displayedObjsArray[_obtainedResultsArray[2] + group3].SetActive(false);
    }
    public virtual void FinishGame() { print("Game was finished"); }
    #endregion
    #region COROUTINES
    #endregion
}
