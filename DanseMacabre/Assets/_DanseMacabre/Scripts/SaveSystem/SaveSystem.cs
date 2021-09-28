using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (HealthSystem player)
    {
        //XmlSerializer serializer = new XmlSerializer(typeof(PlayerSave));
        BinaryFormatter serializer = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saveData.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSave saveData = new PlayerSave(player);

        //serializer.Serialize(stream, saveData);
        serializer.Serialize(stream, saveData);

        stream.Close();
    }

    public static PlayerSave LoadPlayer()
    {
        string path = Application.persistentDataPath + "/saveData.save";
        if (File.Exists(path))
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(PlayerSave));
            BinaryFormatter serializer = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            //PlayerSave data = serializer.Deserialize(stream) as PlayerSave;
            PlayerSave data = serializer.Deserialize(stream) as PlayerSave;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("ErrorSave " + path);
            return null;
        }
    }
}
