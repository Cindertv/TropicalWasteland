using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Awake ()
	{
		if (instance != null)
		{			
			return;
		}
		instance = this;
	}

	public GameObject buildEffect;
	public GameObject sellEffect;

	private TurretBlueprint towerToBuild;
	private Node selectedNode;

	public NodeUI nodeUI;

	public bool CanBuild { get { return towerToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

	public void SelectNode (Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		towerToBuild = null;

		nodeUI.SetTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTurretToBuild (TurretBlueprint turret)
	{
		towerToBuild = turret;
		DeselectNode();
	}

	public TurretBlueprint GetTowerToBuild ()
	{
		return towerToBuild;
	}

}
