using UnityEngine;
using System;

public class BezierSpline : MonoBehaviour {

	[SerializeField]
	private Vector3[] points;

	[SerializeField]
	private BezierControlPointMode[] modes;

	[SerializeField]
	private bool loop;

	public bool Loop {
		get {
			return loop;
		}
		set {
			loop = value;
			if (value == true) {
				modes[modes.Length - 1] = modes[0];
				SetControlPoint(0, points[0]);
			}
		}
	}

	public int ControlPointCount {
		get {
			return points.Length;
		}
	}

	public Vector3 GetControlPoint (int index) {
		return points[index];
	}

	public void SetControlPoint (int index, Vector3 point) {
		if (index % 3 == 0) {
			Vector3 delta = point - points[index];
			if (loop) {
				if (index == 0) {
					points[1] += delta;
					points[points.Length - 2] += delta;
					points[points.Length - 1] = point;
				}
				else if (index == points.Length - 1) {
					points[0] = point;
					points[1] += delta;
					points[index - 1] += delta;
				}
				else {
					points[index - 1] += delta;
					points[index + 1] += delta;
				}
			}
			else {
				if (index > 0) {
					points[index - 1] += delta;
				}
				if (index + 1 < points.Length) {
					points[index + 1] += delta;
				}
			}
		}
		points[index] = point;
		EnforceMode(index);
	}

	public BezierControlPointMode GetControlPointMode (int index) {
		return modes[(index + 1) / 3];
	}

	public void SetControlPointMode (int index, BezierControlPointMode mode) {
		int modeIndex = (index + 1) / 3;
		modes[modeIndex] = mode;
		if (loop) {
			if (modeIndex == 0) {
				modes[modes.Length - 1] = mode;
			}
			else if (modeIndex == modes.Length - 1) {
				modes[0] = mode;
			}
		}
		EnforceMode(index);
	}

	private void EnforceMode (int index) {
		int modeIndex = (index + 1) / 3;
		BezierControlPointMode mode = modes[modeIndex];
		if (mode == BezierControlPointMode.Free || !loop && (modeIndex == 0 || modeIndex == modes.Length - 1)) {
			return;
		}

		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;
		if (index <= middleIndex) {
			fixedIndex = middleIndex - 1;
			if (fixedIndex < 0) {
				fixedIndex = points.Length - 2;
			}
			enforcedIndex = middleIndex + 1;
			if (enforcedIndex >= points.Length) {
				enforcedIndex = 1;
			}
		}
		else {
			fixedIndex = middleIndex + 1;
			if (fixedIndex >= points.Length) {
				fixedIndex = 1;
			}
			enforcedIndex = middleIndex - 1;
			if (enforcedIndex < 0) {
				enforcedIndex = points.Length - 2;
			}
		}

		Vector3 middle = points[middleIndex];
		Vector3 enforcedTangent = middle - points[fixedIndex];
		if (mode == BezierControlPointMode.Aligned) {
			enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
		}
		points[enforcedIndex] = middle + enforcedTangent;
	}

	public int CurveCount {
		get {
			return (points.Length - 1) / 3;
		}
	}

	public Vector3 GetPoint (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}
	
	public Vector3 GetVelocity (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

	public void AddCurve () {
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length + 3);
		point.x += 1f;
		points[points.Length - 3] = point;
		point.x += 1f;
		points[points.Length - 2] = point;
		point.x += 1f;
		points[points.Length - 1] = point;

		Array.Resize(ref modes, modes.Length + 1);
		modes[modes.Length - 1] = modes[modes.Length - 2];
		EnforceMode(points.Length - 4);

		if (loop) {
			points[points.Length - 1] = points[0];
			modes[modes.Length - 1] = modes[0];
			EnforceMode(0);
		}
	}
	
	public void Reset () {
		points = new Vector3[] {
			/*new Vector3(0.8266779f,-0.5090918f,0.02714174f),
			new Vector3(0.776678f,-0.4140918f,0.02714174f),
			new Vector3(0.730678f,-0.6200918f,0.02714174f),
			new Vector3(0.6946779f,-0.5070918f,0.09414174f),
			new Vector3(0.4426779f,-0.5270918f,0.07614174f),
			new Vector3(0.4306779f,-0.5810918f,0.2441417f),
			new Vector3(0.06567794f,-0.5120918f,0.2941417f),
			new Vector3(-0.2013221f,-0.3900918f,0.2351417f),
			new Vector3(-0.3523221f,-0.4828918f,0.1621417f),
			new Vector3(-0.8983221f,-0.5115918f,0.1121417f),
			new Vector3(-1.426322f,-0.5738918f,0.1271417f),
			new Vector3(-1.458322f,-0.3300918f,0.2101417f),
			new Vector3(-0.05532205f,-0.4620918f,0.04114174f),
			new Vector3(0.2206779f,-0.4897918f,0.05214174f),
			new Vector3(0.4496779f,-0.5149918f,-0.01885826f),
			new Vector3(0.6946779f,-0.5070918f,-0.03565826f),
			new Vector3(0.4156779f,-0.4931918f,-0.1958583f),
			new Vector3(0.05967793f,-0.4733918f,-0.2548583f),
			new Vector3(-0.2303221f,-0.3813918f,-0.2028583f),
			new Vector3(-0.3523221f,-0.4828918f,-0.09985827f),
			new Vector3(-0.8983221f,-0.5115918f,-0.06885827f),
			new Vector3(-1.409322f,-0.5649918f,-0.06885827f),
			new Vector3(-1.458322f,-0.3300918f,-0.1428583f)*/
			new Vector3(0f,0f,0f),
			new Vector3(.1f,0f,0f),
			new Vector3(.2f,0f,0f),
			new Vector3(.3f,0f,0f)
		};
		modes = new BezierControlPointMode[] {
			BezierControlPointMode.Free,
			BezierControlPointMode.Free
		};
	}
}