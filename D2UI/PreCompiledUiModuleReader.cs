using FluorineFx.AMF3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class PreCompiledUiModuleReader
{
    private const string HEADER_STR = "D2UI";

    public KeyValuePair<string, Dictionary<string, UiDefinition>> ReadFromStream(MemoryStream inputStream)
    {
        Dictionary<string, UiDefinition> result = new Dictionary<string, UiDefinition>();
        Dictionary<string, int> uiPositions = new Dictionary<string, int>();

        ByteArray reader = new ByteArray(inputStream);
        reader.Position = 0;

        //Checking if it's a valid UI data file
        string headerStr = reader.ReadUTF();

        if (headerStr != HEADER_STR)
        {
            throw new Exception("Malformatted UI data file.");
        }

        //Reading the XML content as in the DM File
        string xmlString = reader.ReadUTF();

        //Counting the number of UI definitions
        short definitionCount = reader.ReadShort();

        //Indexing the UI definitions names and positions
        for (int i = 0; i < definitionCount; i++)
        {
            string uiName = reader.ReadUTF();
            int uiPosition = reader.ReadInt();

            if (uiName == "")
                continue;

            uiPositions.Add(uiName, uiPosition);
        }

        //Reading the UI definitions
        foreach(var uiPos in uiPositions)
        {
            UiDefinition uiDefinition = ReadUiDefinition(inputStream, uiPos.Value);
            result.Add(uiPos.Key, uiDefinition);
        }

        return new KeyValuePair<string, Dictionary<string, UiDefinition>>(xmlString, result);
    }

    private UiDefinition ReadUiDefinition(MemoryStream inputStream, int position)
    {
        ByteArray reader = new ByteArray(inputStream);

        //Set the object encoding to AMF3
        reader.ObjectEncoding = FluorineFx.ObjectEncoding.AMF3;
        // Seek to the position of the UI definition in the input stream
        reader.Position = position;

        // Deserialize the UI definition object (assuming it was serialized as Binary in AS3 in AMF3 Encoding)
        var data = reader.ReadObject();

        UiDefinition uiDefinition =  ParseDefinition(data);

        return uiDefinition;
    }

    private UiDefinition ParseDefinition(object data)
    {

        // Create an instance of UIDefinition
        var uiDefinition = new UiDefinition();

        var dataDict = data as Dictionary<string, object>;

        // Get the properties of UIDefinition using reflection
        PropertyInfo[] properties = typeof(UiDefinition).GetProperties();

        // Copy and parse values from the object to the class instance
        foreach (var property in properties)
        {
            if (!(dataDict).ContainsKey(property.Name))
                continue;

            var value = dataDict[property.Name];

            if (value != null)
            {
                property.SetValue(uiDefinition, value);
            }
        }

        return uiDefinition;
    }
}
