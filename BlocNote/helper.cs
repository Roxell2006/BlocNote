using BlocNote.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlocNote
{
    public class Helper
    {
        public static List<Note> LoadNotes(string _fileName)
        {
            if (File.Exists(_fileName))
            {
                string jsonFile = File.ReadAllText(_fileName);
                return JsonConvert.DeserializeObject<List<Note>>(jsonFile);
            }
            else
                return null;
            
        }
        public static void SaveNotes(string _fileName, List<Note> notes)
        {
            string jsonFichier = JsonConvert.SerializeObject(notes);
            File.WriteAllText(_fileName, jsonFichier);
        }
    }
}
