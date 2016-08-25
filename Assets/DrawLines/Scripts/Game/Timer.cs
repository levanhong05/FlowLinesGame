using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{
		/// <summary>
		/// Text Component
		/// </summary>
		public Text uiText;

		public Text sleepTimeText;

		/// <summary>
		/// The time in seconds.
		/// </summary>
		public static int timeInSeconds;

		/// <summary>
		/// Whether the Timer is running
		/// </summary>
		private bool isRunning;

		void Awake ()
		{
				if (uiText == null) {
					uiText = GetComponent<Text> ();
				}

				if (sleepTimeText == null) {
					sleepTimeText = GameObject.Find ("SleepTime").GetComponent<Text> ();
				}
				///Start the Timer
				Start ();
		}

		/// <summary>
		/// Start the Timer.
		/// </summary>
		public void Start ()
		{
				if (!isRunning) {
						isRunning = true;
						timeInSeconds = 124;
						StartCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Stop the Timer.
		/// </summary>
		public void Stop ()
		{
				if (isRunning) {
						isRunning = false;
						StopCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Wait Coroutine.
		/// </summary>
		private IEnumerator Wait ()
		{
				while (isRunning) {
						timeInSeconds--;
						if (timeInSeconds < 0) {
							/// Stop the Timer.
							Stop ();
							timeInSeconds = -1;
							Debug.Log ("Game over...");
							StopAllCoroutines ();
							///Show the black area
							BlackArea.Show ();

							///Show the awesome dialog
							GameObject.FindObjectOfType<AwesomeDialog> ().GameOver ();
							GameObject.FindObjectOfType<AwesomeDialog> ().Show();
							Debug.Log ("You incompleted level " + TableLevel.wantedLevel.ID);
						} else {
							if (timeInSeconds < 120) {
								ApplyTime ();
							} else if (timeInSeconds == 120) {
								BlackArea.Hide ();
								sleepTimeText.gameObject.SetActive (false);
							} else {
								sleepTimeText.text = (timeInSeconds - 120).ToString ();
							}
							yield return new WaitForSeconds (1);
						}
				}
		}

		/// <summary>
		/// Applies the time into TextMesh Component.
		/// </summary>
		private void ApplyTime ()
		{
				if (uiText == null) {
						return;
				}
				uiText.text = "Time : " + timeInSeconds;
		}
}