using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("AssetRoot")]
public	class FageAssetRoot {
	private	static FageAssetRoot _instance;
	public	static FageAssetRoot Instance { get { return _instance; } }
	
	[XmlAttribute("url")]
	public	string				url;
	[XmlElement("AssetNode")]
	public	FageAssetNode[]		bundles;
	
	private	Dictionary<string, FageAssetNode> 		_dictionary;
	
	public	FageAssetRoot() {
		bundles = new FageAssetNode[0];
		_dictionary = new Dictionary<string, FageAssetNode>();
	}
	
	private	void Hashing() {
		_dictionary.Clear();
		
		foreach(FageAssetNode bundle in bundles) {
			_dictionary.Add(bundle.id, bundle);
		}
	}

	public	FageAssetNode FindAssetNode(string id) {
		if (_dictionary.ContainsKey(id)) {
			return _dictionary[id];
		} else {
			return null;
		}
	}
	
	public	static void LoadFromText(string text) {
		XmlSerializer serializer = new XmlSerializer(typeof(FageAssetRoot));
		_instance = serializer.Deserialize(new StringReader(text)) as FageAssetRoot;
		_instance.Hashing();
	}
}

public	class FageAssetNode {
	[XmlAttribute("id")]
	public	string	id;
	[XmlAttribute("url")]
	public	string	url;
	[XmlAttribute("version")]
	public	int		version;
	[XmlAttribute("dynamic")]
	public	bool	isDynamic;
	[XmlAttribute("cached")]
	public	bool	isCached;
	public	bool	isStatic { get { return !isDynamic; } }
}