using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSettings
{
    public static void SaveVolume(GameObject gameObject)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/volume.dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(gameObject);

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static SettingsData LoadSettingsData()
    {
        try
        {
            string path = Application.persistentDataPath + "/volume.dat";

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(fileStream) as SettingsData;
            fileStream.Close();

            return data;
        }
        catch(IOException exception)
        {
            Debug.LogWarning("Problem with loading "+exception.Message);
            return null;
        }
    }
}
