using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor;

public class AnalyticsManager : MonoBehaviour
{
	public GameStatus gameStatus;
	
	void Start () 
	{
		LoadGameStatusFromFile();
	}

	//Provides a template game status file
	static GameStatus GenerateGameStatusFile()
	{
		return new GameStatus(new int[]{0, 5, 3, 1});
	}

	//To be used manually when you don't have an XML file to read from
	[MenuItem("NDA/Analytics/Generate Game Status File")]
	public static void SaveGeneratedGameStatusFile()
	{
		var path = Path.Combine(Application.dataPath, "Xml/GameStatus.xml");
        
		using (var stream = new FileStream(path, FileMode.Create))
		{
			var serializer = new XmlSerializer(typeof(GameStatus));

			serializer.Serialize(stream, GenerateGameStatusFile());
		}
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
