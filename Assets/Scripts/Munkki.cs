using UnityEngine;
using System.Collections;

public class Munkki : Interaction {

	// Use this for initialization
	void Start () {
	
	}

    public override void OnInteraction(Interaction present)
    {
        Destroy(gameObject);
    }

    public override string Description(Interaction present)
    {
        return "Eat munkki!";
    }
}
