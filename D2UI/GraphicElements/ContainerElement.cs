using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace D2UIReader.D2UI.GraphicElements
{
    public class ContainerElement : BasicElement
    {
        List<ContainerElement> parsedChilds;

        public object[] childs
        {
            set
            {
                parsedChilds = new List<ContainerElement>();
                if (value != null && value.Any())
                    foreach (var item in value)
                    {
                        parsedChilds.Add(ParseContainerElement(item));
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
}
