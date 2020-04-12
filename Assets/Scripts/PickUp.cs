using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

	public enum PickUps
	{
		food,
		meds,
		water
	}

	[SerializeField] PickUps type;
	[SerializeField] float minValue = 5f;
	[SerializeField] float maxValue = 20f;

	float value = 0;

	private void Start() {
		value = Random.Range(minValue, maxValue);
	}

	public PickUps GetPickupType() {
		return type;
	}

	public float GetValue() {
		return value;
	}


}
