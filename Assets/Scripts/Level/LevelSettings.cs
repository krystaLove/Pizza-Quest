﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public GameObject thisLevel;
    public Transform spawnPoint;
    public GameObject spriteBg;
    public AudioClip musicTheme;

    [Range(5, 10)]
    public float cameraSize = 5.5f;
}
