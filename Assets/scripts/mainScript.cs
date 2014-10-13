using UnityEngine;
using System.Collections;

public class mainScript : MonoBehaviour
{

	// public class variables
	public int mPublicSampleVar;

	// protected class variables
	protected int mProtectedSampleVar;

	// private class methods
	private int mPrivateSampleVar;


	// Use this for initialization
	void Start()
	{
		this.mPrivateSampleVar = 0;
		this.mProtectedSampleVar = 0;
		this.mPublicSampleVar = 0;
	}
	
	// Update is called once per frame
	void Update()
	{
		this.mPrivateSampleVar++;
		this.mProtectedSampleVar++;
		this.mPublicSampleVar++;
	}
}
