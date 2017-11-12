using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Outline : MonoBehaviour
{
    public OutlineEffect outlineEffect;

    //inspector
	public int color;
	public bool eraseRenderer = true;
	public bool mouseOver = false;

	//overrides to hold glows outside of mouse
	private bool overriden = false;

	[HideInInspector]
	public int originalLayer;
	[HideInInspector]
	public Material originalMaterial;

	void Awake()
	{
		//attach to parent LevelController
		Transform t = transform;
		while (t.parent != null)
		{
			LevelController lManager = t.parent.GetComponent<LevelController>();
			if (lManager != null) {
				lManager.objectOutlines.Add(this);
				break;
			}
			t = t.parent.transform;
		}
	}

    void OnEnable()
    {
		if(outlineEffect == null)
			outlineEffect = Camera.main.GetComponent<OutlineEffect>();
		outlineEffect.AddOutline(this);
    }

    void OnDisable()
    {
        outlineEffect.RemoveOutline(this);
    }

    public void OnMouseOver()
    {
    	if (mouseOver && eraseRenderer) {
    		eraseRenderer = false;
    	}
    }

    public void OnMouseExit()
    {
    	if (mouseOver && !overriden) {
    		eraseRenderer = true;
    	}
    }

    public void ForceGlow ()
    {
    	overriden = true;
    	eraseRenderer = false;
    }
	public void ForceDisable ()
	{
		mouseOver = false;
		eraseRenderer = true;
	}
	public void ResetForcedGlow ()
	{
		overriden = false;
		//check for mouse over
		RaycastHit hit;
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(mouseRay, out hit)) {
			if (hit.collider.gameObject == gameObject) {
				return;
			}
		}
		//reset glow as we are not mousing over
		eraseRenderer = true;
	}
}
