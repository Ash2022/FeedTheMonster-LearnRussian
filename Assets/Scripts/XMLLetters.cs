using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

[XmlRoot("XMLLetters")]
public class XMLLetters {

	[XmlArray("letters")]
	[XmlArrayItem("letter")]
	public string[] letters;




	public IEnumerator LoadFromResources(string path){
		TextAsset xmlDataFile = new TextAsset ();
		xmlDataFile = (TextAsset)Resources.Load (path, typeof(TextAsset));
		MemoryStream ms = new MemoryStream (xmlDataFile.bytes);

		XmlTextReader reader;
		yield return reader = new XmlTextReader  (ms);

		SerializeLevelFromXML (path ,reader);
		yield return true;
	}

	private void SerializeLevelFromXML(string path, XmlReader reader)
	{
		Dictionary<char, string> ltrs = new Dictionary<char, string>();

		var serializer = new XmlSerializer(typeof(XMLLetters));
		XMLLetters xmlLevel = serializer.Deserialize(reader) as XMLLetters;

		foreach (string ltr in xmlLevel.letters) {
			ltrs.Add (ltr[0], ltr);
		}



	}
}
