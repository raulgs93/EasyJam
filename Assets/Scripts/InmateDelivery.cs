using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmateDelivery : MonoBehaviour
{


	[SerializeField] InmateAI inmateAssigned;
	[SerializeField] int scoreXCrate = 5;

	UIController ui;

	private void Start() {
		ui = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		
		PickUp pickUp = collision.GetComponent<PickUp>();

		if (pickUp == null) return;
		else {

			ui.AddScore(scoreXCrate);

			switch (pickUp.GetPickupType()) {

				case PickUp.PickUps.food:
					inmateAssigned.AddHunger(pickUp.GetValue());
					break;

				case PickUp.PickUps.meds:
					inmateAssigned.AddHealth(pickUp.GetValue());
					break;

				case PickUp.PickUps.water:
					inmateAssigned.AddThirst(pickUp.GetValue());
					break;

				default:

					print("not yet implemented");
					break;
			}


			Destroy(pickUp.gameObject);
		}
	}
}
