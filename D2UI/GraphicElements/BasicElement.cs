using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace D2UIReader.D2UI.GraphicElements
{
    public class BasicElement
    {
        
        public string name;
        public uint strata = 1;
        public SizeElement parsedSize;
        public SizeElement parsedMinSize;
        public SizeElement parsedMaxSize;
        public List<LocationElement> parsedAnchors;
        public object[] @event; // Use "@" to use reserved word "event"
        public List<PropertyElement> parsedProperties;
        public string className;
        public int cachedWidth = 2147483647;
        public int cachedHeight = 2147483647;
        public int cachedX = 2147483647;
        public int cachedY = 2147483647;

        public object size
        {
            set
            {
                parsedSize = ParseSizeElement(value);
            }
        }

        public object minSize
        {
            set
            {
                parsedMinSize = ParseSizeElement(value);
            }
        }

        public object maxSize
        {
            set
            {
                parsedMaxSize = ParseSizeElement(value);
            }
        }

        public object[] anchors
        {
            set
            {
                parsedAnchors = new List<LocationElement>();
                if (value != null && value.Any())
                    foreach (var item in value)
                    {
                        parsedAnchors.Add(ParseLocationElement(item));
                    }
            }
        }

        private SizeElement ParseSizeElement(object data)
        {

            // Create an instance of SizeElement
            var sizeElement = new SizeElement();

            var dataDict = data as Dictionary<string, object>;

            // Get the properties of SizeElement using reflection
            PropertyInfo[] properties = typeof(SizeElement).GetProperties();

            // Copy and parse values from the object to the class instance
            foreach (var property in properties)
            {
                if (!(dataDict).ContainsKey(property.Name))
                    continue;

                var value = dataDict[property.Name];

                if (value != null)
                {
                    property.SetValue(sizeElement, value);
                }
            }

            return sizeElement;
        }

        private PropertyElement ParsePropertyElement(object data)
        {

            // Create an instance of PropertyElement
            var propertyElement = new PropertyElement();

            var dataDict = data as Dictionary<string, object>;

            // Get the properties of PropertyElement using reflection
            PropertyInfo[] properties = typeof(PropertyElement).GetProperties();

            // Copy and parse values from the object to the class instance
            foreach (var property in properties)
            {
                if (!(dataDict).ContainsKey(property.Name))
                    continue;

                var value = dataDict[property.Name];

                if (value != null)
                {
                    property.SetValue(propertyElement, value);
                }
            }

            return propertyElement;
        }

        private LocationElement ParseLocationElement(object data)
        {
            // Create an instance of LocationElement
            var locationElement = new LocationElement();

            var dataDict = data as Dictionary<string, object>;

            // Get the properties of LocationElement using reflection
            PropertyInfo[] properties = typeof(LocationElement).GetProperties();

            // Copy and parse values from the object to the class instance
            foreach (var property in properties)
            {
                if (!(dataDict).ContainsKey(property.Name))
                    continue;

                var value = dataDict[property.Name];

                if (value != null)
                {
                    property.SetValue(locationElement, value);
                }
            }

            return locationElement;
        }

    }
}
