using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelController : MonoBehaviour {

    public RectTransform scrollPanel;
    public RectTransform[] panels;
    public RectTransform center;

    private float[] distance;
    private bool dragging = false;
    private int panelDistance;
    private int minPanelNum;

	void Start () {
        int panelLength = panels.Length;
        distance = new float[panelLength];

        panelDistance = (int)Mathf.Abs(panels[1].GetComponent<RectTransform>().anchoredPosition.x - panels[0].GetComponent<RectTransform>().anchoredPosition.x);
	}
	
	void Update () {
	    for (int i = 0; i < panels.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - panels[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < panels.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minPanelNum = a;
            }
        }

        if (!dragging)
        {
            LerpToPanel(minPanelNum * -panelDistance);
        }
	}

    void LerpToPanel (int position)
    {
        float newX = Mathf.Lerp(scrollPanel.anchoredPosition.x, position, Time.deltaTime * 30f);
        Vector2 newPosition = new Vector2(newX, scrollPanel.anchoredPosition.y);

        scrollPanel.anchoredPosition = newPosition;
    }

    public void StartDrag ()
    {
        dragging = true;
    }
    
    public void EndDrag()
    {
        dragging = false;
    }
}
