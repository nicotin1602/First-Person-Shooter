﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSelfDestruction : MonoBehaviour {
    public float destroyTime = 0.2f;

    public Vector3 randomizeIntensity = new Vector3(0.5f, 0.5f, 0f);

	void Start () {
        Destroy(gameObject, destroyTime);
        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x), Random.Range(-randomizeIntensity.y, randomizeIntensity.y), 0);
		
	}
	
	
}
