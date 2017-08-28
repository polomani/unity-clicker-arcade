using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Repository : MonoBehaviour {

    private static DataObject data = null;
    private const string FILE_NAME = "data.save";

    public static DataObject Data
    {
        get
        {
            if (data == null) Load();
            return data;
        }
        set
        {
            data = value;
            Save();
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_NAME);
        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + FILE_NAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FILE_NAME, FileMode.Open);
            data = (DataObject)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            data = new DataObject();
            Save();
        }
    }
}
