using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;
using System.Xml;
using System.Xml.Serialization;

public class AnalyticsManager : MonoBehaviour
{
	public GameStatus gameStatus;
	
	void Start () 
	{
		LoadGameStatusFromFile();
	}

	void LoadGameStatusFromFile()
	{
		var path = Path.Combine(Application.dataPath, "Xml/GameStatus.xml");
		using (var stream = new FileStream(path, FileMode.Open))
		{
			var serializer = new XmlSerializer(typeof(GameStatus));
			gameStatus = (GameStatus) serializer.Deserialize(stream);
		}
	}
}
