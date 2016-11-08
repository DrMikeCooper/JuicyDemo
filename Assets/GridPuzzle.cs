using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class GridPuzzle : MonoBehaviour {

    public int columns;
    public int rows;
    public GameObject piece;

    [HideInInspector]
    GridPuzzlePiece current;

    AudioSource audioSource;
    public AudioClip sparkle;
    public AudioClip select;
    public AudioClip deselect;

	// Use this for initialization
	void Start () {
        Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };

        for (int i = 0; i < columns; i++)
        {
            float x0 = ((((float)i) / columns) - 0.5f) * columns;
            for (int j = 0; j < rows; j++)
            {
                float y0 = ((((float)j) / rows) - 0.5f) * rows;
                GameObject obj = Instantiate<GameObject>(piece);
                obj.transform.localPosition = new Vector3(x0, y0, 0);
                obj.transform.parent = transform;
                obj.name = "piece_" + i + "_" + j;
                obj.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0,4)];
                obj.GetComponent<GridPuzzlePiece>().puzzle = this;
            }
        }

        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GridPuzzlePiece piece = hit.collider.GetComponent<GridPuzzlePiece>();
                if (piece != null)
                {
                    if (piece == current)
                    {
                        // shrink back down
                        iTween.ScaleTo(piece.gameObject, iTween.Hash("time", 1, "scale", new Vector3(1,1,1), "easeType", "easeOutBack"));
                        current = null;
                        audioSource.clip = deselect;
                        audioSource.Play();
                    }
                    else
                    {
                        if (current == null)
                        {
                            // scale up
                            iTween.ScaleTo(piece.gameObject, iTween.Hash("time", 1, "scale", new Vector3(1.4f, 1.4f, 1.4f), "easeType", "easeOutBack"));
                            current = piece;
                            audioSource.clip = select;
                            audioSource.Play();
                        }
                        else
                        {
                            // swap piece and current and scale current down
                            iTween.ScaleTo(current.gameObject, iTween.Hash("time", 1, "scale", new Vector3(1, 1, 1), "easeType", "easeOutBack"));
                            iTween.MoveTo(piece.gameObject, iTween.Hash("position", current.transform.position, "time", 0.5, "easeType", "easeInOutCirc"));
                            iTween.MoveTo(current.gameObject, iTween.Hash("position", piece.transform.position, "time", 0.5, "easeType", "easeInOutCirc"));
                            piece.Sparkle();
                            current.Sparkle();
                            audioSource.clip = sparkle;
                            audioSource.Play();
                            current = null;
                        }
                    }
                }

            }
        }
	}
}
