using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //Path for binary file
    readonly static string path = Application.persistentDataPath + "/player.stats";

    //Save player data to binary file
    public static void SavePlayer (int player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Create a new file at the path
        FileStream stream = new FileStream(path, FileMode.Create);
        //Attaining player data
        PlayerData data = new PlayerData(player);

        //Convert player data to binary and store it at the path
        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Load player data from binary file
    public static PlayerData LoadPlayer ()
    {
        //Check if the file does exsist
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            //Open the file at the path
            FileStream stream = new FileStream(path, FileMode.Open);

            //Convert the binary player data back to readable data and store it in the "data" variable
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else
        {
            //If the user has no saved data, return intial values
            PlayerData player = new PlayerData(0);

            return player;
        }
    }
}
