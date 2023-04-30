using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private int numberOfPapers = 3;


    public int GetNumberOfPapers() {
        return numberOfPapers;
    }
}
