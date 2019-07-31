using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public string fileName = "GameData.xml";

    private GameData data = new GameData();
    private void Start()
    {
        string path = Application.dataPath +"/" + fileName;
        Load(path);
    }

    public void Load(string path)
    {
        var serializer = new XmlSerializer(typeof(GameData));
        var stream = new FileStream(path, FileMode.Open);
        data = serializer.Deserialize(stream) as GameData;
        stream.Close();
    }
    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(GameData));
        // stream is highway between application and string stream
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
        Debug.Log("File Saved Succsessfully to" + path);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            player = FindObjectOfType<PlayerScript>().transform;
            data.pos = player.position;
            data.rot = player.rotation;
            data.dialogue = new string[]
            {
                "Hello",
                "World"

            };
            Save(Application.dataPath + "/" + fileName);
        }
    }
}
