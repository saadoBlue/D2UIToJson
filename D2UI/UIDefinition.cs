using D2UIReader.D2UI.GraphicElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class UiDefinition
{
    public string name { get; set; }
    public bool debug { get; set; } = false;

    public List<ContainerElement> parsedGraphicTree { get; set; }
    public object[] kernelEvents { get; set; }

    public List<string> parsedShortcutsEvents { get; set; }

    public Dictionary<string, string> parsedConstants { get; set; }
    public string className { get; set; }
    public bool useCache { get; set; }
    public bool usePropertiesCache { get; set; }
    public bool modal { get; set; }
    public bool giveFocus { get; set; }
    public bool transmitFocus { get; set; }
    public bool scalable { get; set; }
    public bool labelDebug { get; set; }
    public bool fullscreen { get; set; }
    public bool setOnTopOnClick { get; set; }

    public object[] shortcutsEvents
    {
        set
        {
            parsedShortcutsEvents = new List<string>();
            if (value != null && value.Any())
                foreach (var item in value)
                {
                    parsedShortcutsEvents.Add(item.ToString());
                }
        }
    }

    public Dictionary<string, object> constants
    {
        set
        {
            parsedConstants = new Dictionary<string, string>();
            if (value != null && value.Any())
                foreach (var item in value)
                {
                    parsedConstants.Add(item.Key, item.Value.ToString());
                }
        }
    }

    public object[] graphicTree
    {
        set
        {
            parsedGraphicTree = new List<ContainerElement>();
            if (value != null && value.Any())
                foreach (var item in value)
                {
                    parsedGraphicTree.Add(ParseContainerElement(item));
                }
        }
    }


    private ContainerElement ParseContainerElement(object data)
    {

        // Create an instance of ContainerElement
        var containerElement = new ContainerElement();

        var dataDict = data as Dictionary<string, object>;

        // Get the properties of ContainerElement using reflection
        PropertyInfo[] properties = typeof(ContainerElement).GetProperties();

        // Copy and parse values from the object to the class instance
        foreach (var property in properties)
        {
            if (!(dataDict).ContainsKey(property.Name))
                continue;

            var value = dataDict[property.Name];

            if (value != null)
            {
                property.SetValue(containerElement, value);
            }
        }

        return containerElement;
    }
}
