using UnityEngine;

public class SplineWalkerArray : MonoBehaviour 
{

	public BezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;

	private float[] progress;
	private bool goingForward = true;
	
    private int numSelectors;
    public GameObject[] selectorArr;
    public GameObject selector; //selected in the editor
	
	
    void Start()
	{ 
		numSelectors = 1;
        selectorArr = new GameObject[numSelectors];
		progress = new float[numSelectors];		
        for (int i = 0; i < numSelectors; i++)
        {
            GameObject go = Instantiate(selector, new Vector3((float)i, 1, 0), Quaternion.identity) as GameObject;
			if (i > 0){
				progress[i] = progress[i-1] + 1f * Time.deltaTime / duration;
			}
			Vector3 position = spline.GetPoint(progress[i]);
            selectorArr[i] = go;
			selectorArr[i].transform.localPosition = position;	
        }
    }	

	private void Update () {
		for (int i = 0; i < numSelectors; i++){
			if (goingForward) {
				progress[i] += Time.deltaTime / duration;
				if (progress[i] > 1f) {
					if (mode == SplineWalkerMode.Once) {
						progress[i] = 1f;
					}
					else if (mode == SplineWalkerMode.Loop) {
						progress[i] -= 1f;
					}
					else {
						progress[i] = 2f - progress[i];
						goingForward = false;
					}
				}
			}
			else {
				progress[i] -= Time.deltaTime / duration;
				if (progress[i] < 0f) {
					progress[i] = -progress[i];
					goingForward = true;
				}
			}

			Vector3 position = spline.GetPoint(progress[i]);
			selectorArr[i].transform.localPosition = position;
			if (lookForward) {
				transform.LookAt(position + spline.GetDirection(progress[i]));
			}
		}
	}
}