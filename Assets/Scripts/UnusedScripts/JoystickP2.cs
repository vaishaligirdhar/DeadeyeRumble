using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{

		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public int MovementRange = 80;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "p2x"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "p2y"; // The name given to the vertical axis for the cross platform input
		public string P2_horizontalAxisName = "P2_Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string P2_verticalAxisName = "P2_Vertical"; // The name given to the vertical axis for the cross platform input
		public int isPlayer1; // Use this to flip x-axis depending on which player this script belongs to

		Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

		Vector3 n_StartPos; //Still needed for OnDrag();
		/*bool n_UseX; // Toggle for using the x axis
		bool n_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis n_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis n_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

		Vector3 lm_StartPos;
		Vector3 ln_StartPos;
		*/
		void Start()
		{
			//Debug.Log ("transform.parent.localPosition: " + transform.parent.localPosition.x.ToString () + " " + transform.parent.localPosition.y.ToString () + " " + transform.parent.localPosition.z.ToString ());
			//Debug.Log ("transform.position: " + transform.position.x.ToString () + " " + transform.position.y.ToString () + " " + transform.position.z.ToString ());
			//Debug.Log ("transform.localPosition: " + transform.localPosition.x.ToString () + " " + transform.localPosition.y.ToString () + " " + transform.localPosition.z.ToString ());

			//m_StartPos = transform.position;
			//m_StartPos = new Vector3 (145, 90, transform.position.z);
			//m_StartPos = transform.localPosition;
			//n_StartPos = transform.position; //Still needed for OnDrag();
			//n_StartPos = transform.localPosition;

			//Debug.Log ("m_StartPos: " + m_StartPos.x.ToString() + " " + m_StartPos.y.ToString() + " " + m_StartPos.z.ToString());
			//Debug.Log ("n_StartPos: " + n_StartPos.x.ToString() + " " + n_StartPos.y.ToString() + " " + n_StartPos.z.ToString());

			//lm_StartPos = transform.localPosition;
			//ln_StartPos = transform.localPosition;
			//Debug.Log ("lm_StartPos: " + lm_StartPos.x.ToString() + " " + lm_StartPos.y.ToString() + " " + lm_StartPos.z.ToString());
			//Debug.Log ("ln_StartPos: " + ln_StartPos.x.ToString() + " " + ln_StartPos.y.ToString() + " " + ln_StartPos.z.ToString());

			//Debug.Log("normal pos: " + transform.InverseTransformPoint(
			if (transform.parent.gameObject.name == "Player1UI") {
				//Debug.Log ("X-axis map unchanged: " + isPlayer1.ToString ());
				isPlayer1 = 1;
			} else if (transform.parent.gameObject.name == "Player2UI") {
				//Debug.Log ("X-axis map flipped: " + isPlayer1.ToString ());
				isPlayer1 = -1;
			} else {
				Debug.Log ("[Error] Hierarchy names/order changed");
				isPlayer1 = 1;
			}
			//CreateVirtualAxes();



		}

		void OnEnable(){
			m_StartPos = transform.localPosition;
			n_StartPos = transform.localPosition;
			CreateVirtualAxes();

		}

		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			//Debug.Log ("delta: " + delta.x.ToString() + " " + delta.y.ToString() + " " + delta.z.ToString());
			if (m_UseX)
			{
				//if (isPlayer1 == -1)
				m_HorizontalVirtualAxis.Update (-delta.x);
				//else if (isPlayer1 == 1)
				//	m_HorizontalVirtualAxis.Update (delta.x);
				//else {
				//	Debug.LogError ("Invalid isPlayer1 value");
				//}

			}

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}


			/*var delta1 = n_StartPos - value;
			delta1.y = -delta1.y;
			delta1 /= MovementRange;
			if (n_UseX)
			{
				n_HorizontalVirtualAxis.Update(-delta1.x);
			}

			if (n_UseY)
			{
				n_VerticalVirtualAxis.Update(delta1.y);
			}*/
		}

		void CreateVirtualAxes()
		{
			// set axes to use
			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (m_UseX)
			{
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}

			/*if (n_UseX)
			{
				n_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(P2_horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(n_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				n_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(P2_verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(n_VerticalVirtualAxis);
			}*/
		}


		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;
			/*Debug.Log ("data.position: " + data.position.x.ToString() + " " + data.position.y.ToString());
			Debug.Log ("data.pressPosition: " + data.pressPosition.x.ToString() + " " + data.pressPosition.y.ToString());
			Debug.Log ("transform.position: " + transform.position.x.ToString() + " " + transform.position.y.ToString() + " " + transform.position.z.ToString());
			Debug.Log ("transform.localPosition: " + transform.localPosition.x.ToString() + " " + transform.localPosition.y.ToString() + " " + transform.localPosition.z.ToString());

			Debug.Log ("m_StartPos: " + m_StartPos.x.ToString() + " " + m_StartPos.y.ToString() + " " + m_StartPos.z.ToString());
			//Debug.Log ("n_StartPos: " + n_StartPos.x.ToString() + " " + n_StartPos.y.ToString() + " " + n_StartPos.z.ToString());

			//Debug.Log ("lm_StartPos: " + lm_StartPos.x.ToString() + " " + lm_StartPos.y.ToString() + " " + lm_StartPos.z.ToString());
			//Debug.Log ("ln_StartPos: " + ln_StartPos.x.ToString() + " " + ln_StartPos.y.ToString() + " " + ln_StartPos.z.ToString());

			Debug.Log ("transform.parent.localPosition: " + transform.parent.localPosition.x.ToString () + " " + transform.parent.localPosition.y.ToString () + " " + transform.parent.localPosition.z.ToString ());
			*/
			//Vector3 dataAsVector = new Vector3 (data.position.x, data.position.y, transform.position.z); 

			if (m_UseX)
			{
				int delta = (int)(data.position.x - m_StartPos.x);
				//delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (m_UseY)
			{
				int delta = (int)(data.position.y - m_StartPos.y);
				//delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}

			/*if (n_UseX)
			{
				int delta1 = (int)(data.position.x - n_StartPos.x);
				//delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta1;
			}

			if (n_UseY)
			{
				int delta1 = (int)(data.position.y - n_StartPos.y);
				//delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta1;
			}*/
			transform.localPosition = Vector3.ClampMagnitude(new Vector3(isPlayer1*newPos.x, newPos.y, newPos.z), MovementRange) + n_StartPos;
			UpdateVirtualAxes(transform.localPosition);
		}


		public void OnPointerUp(PointerEventData data)
		{
			transform.localPosition = m_StartPos;
			UpdateVirtualAxes(m_StartPos);

			//transform.position = n_StartPos;
			//UpdateVirtualAxes(n_StartPos);
		}


		public void OnPointerDown(PointerEventData data) { }

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis.Remove();
			}

			/*if (n_UseX)
			{
				n_HorizontalVirtualAxis.Remove();
			}
			if (n_UseY)
			{
				n_VerticalVirtualAxis.Remove();
			}*/
		}
	}

}