using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmateDelivery : MonoBehaviour
{


	[SerializeField] InmateAI[] inmateAssigned;
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
					if (inmateAssigned.Length > 1) {
						foreach (InmateAI inmate in inmateAssigned) {
							inmate.AddHunger(pickUp.GetValue() / inmateAssigned.Length);
						}
					}
					else {
						inmateAssigned[0].AddHunger(pickUp.GetValue());
					}
					break;

				case PickUp.PickUps.meds:
					if (inmateAssigned.Length > 1) {
						foreach (InmateAI inmate in inmateAssigned) {
							inmate.AddHealth(pickUp.GetValue() / inmateAssigned.Length);
						}
					}
					else {
						inmateAssigned[0].AddHealth(pickUp.GetValue());
					}
					break;

				case PickUp.PickUps.water:
					if (inmateAssigned.Length > 1) {
						foreach (InmateAI inmate in inmateAssigned) {
							inmate.AddThirst(pickUp.GetValue() / inmateAssigned.Length);
						}
					}
					else {
						inmateAssigned[0].AddThirst(pickUp.GetValue());
					}
					break;

				default:

					print("not yet implemented");
					break;
			}

			FindObjectOfType<SoundManager>().PlaySound("powerUp");

			Destroy(pickUp.gameObject);
		}
	}
}
