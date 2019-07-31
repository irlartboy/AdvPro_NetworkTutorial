using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class GameData 
{
    [SerializeField]
    public Vector3 pos;
    public Quaternion rot;
    [XmlArray("Dialgoue")]
    [XmlArrayItem("Text")]
    public string[] dialogue;
}
