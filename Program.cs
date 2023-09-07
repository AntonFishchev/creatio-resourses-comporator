using System.Xml;

var minDoc = new XmlDocument();
minDoc.Load(@"../../../assets/min.xml");

var maxDoc = new XmlDocument();
maxDoc.Load(@"../../../assets/max.xml");

var resultDoc = new XmlDocument();
resultDoc.Load(@"../../../assets/result.xml");

// Получение корневого элемента Items.
var minItems = minDoc.DocumentElement;
var maxItems = maxDoc.DocumentElement;

var count = 0;

foreach (XmlNode itemMax in maxItems.ChildNodes)
{
	var name = itemMax.Attributes.GetNamedItem("Name").Value;
	var value = itemMax.Attributes.GetNamedItem("Value").Value;

	foreach (XmlNode itemMin in minItems.ChildNodes)
	{	
		if (itemMin.Attributes.GetNamedItem("Name").Value == itemMax.Attributes.GetNamedItem("Name").Value)
		{
			value = itemMin.Attributes.GetNamedItem("Value").Value;
			count++;
		}
	}

	Add(resultDoc, name, value);
}

resultDoc.Save(@"../../../result.xml");

Console.WriteLine(count);

void Add(XmlDocument doc, string name, string value)
{
	var items = resultDoc.DocumentElement;
	var item = doc.CreateElement("Item");

	var nameAttr = doc.CreateAttribute("Name");
	nameAttr.Value = name;
	var valueAttr = doc.CreateAttribute("Value");
	valueAttr.Value = value;

	item.Attributes.Append(nameAttr);
	item.Attributes.Append(valueAttr);

	items.AppendChild(item);
}