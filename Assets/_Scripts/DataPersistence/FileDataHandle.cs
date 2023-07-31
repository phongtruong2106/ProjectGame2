using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandle : ScriptableObject
{
   private string dataDirPath = "";

   private string dataFileName = "";

   public FileDataHandle(string dataDirPath, string dataFileName)
   {
        this.dataDirPath =  dataDirPath;
        this.dataFileName = dataFileName;
   }

   public GameData Load()
   {
        //use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from json back into the C# obj
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e); 
            }
        }
        return loadedData;
   }

   public void Save(GameData data)
   {
     string fullPath = Path.Combine(dataDirPath, dataFileName);
     try
     {
        //create the directory the file will be written to if doesn't already exist
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        //serialize the C# game data Object into Json
        string dataToStore = JsonUtility.ToJson(data, true);

        //write the serialized data to the file
        using(FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using(StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
     }
     catch(Exception e)
     {
        Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
     }
   }

//thêm một phương thức Init vào lớp FileDataHandle để có thể khởi tạo đối tượng và truyền dữ liệu vào từ bên ngoài:
   public void Init(string dataDirPath, string dataFileName)
{
    this.dataDirPath = dataDirPath;
    this.dataFileName = dataFileName;
}
}
