using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint fireTower;
	public TurretBlueprint lightingTower;
	public TurretBlueprint iceTower;

	BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance;
	}

	public void SelectFireTurret ()
	{
		Debug.Log("Fire Tower Selected");
		buildManager.SelectTurretToBuild(fireTower);
	}

	public void SelectLightningTower()
	{
		Debug.Log("Lightning Tower Selected");
		buildManager.SelectTurretToBuild(lightingTower);
	}

	public void SelectIceTower()
	{
		Debug.Log("Ice Tower Selected");
		buildManager.SelectTurretToBuild(iceTower);
	}

}
