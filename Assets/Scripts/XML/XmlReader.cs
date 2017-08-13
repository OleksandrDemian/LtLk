using System.Xml;

public class XmlReader
{
    /*
    public static Wave GetWave(string id)
    {
        TextAsset file = (TextAsset)Resources.Load("Waves");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(file.text);

        try
        {
            XmlNodeList nodes = doc.GetElementsByTagName("wave");
            XmlNodeList events = GetNode(id, nodes).ChildNodes;
            Wave wave = new Wave();
            wave.SetID(int.Parse(id));

            for (int i = 0; i < events.Count; i++)
            {
                WaveEvent wEvent = ProcessWaveEvent(events[i]);
                wave.AddEvent(wEvent);
            }
            
            return wave;
        }
        catch
        {
            return null;
        }
    }

    private static WaveEvent ProcessWaveEvent(XmlNode node)
    {
        WaveEvent wEvent = null;

        string tag = node.Name;

        switch (tag)
        {
            case "dialog":
                string id = node.Attributes["ref"].InnerText;
                wEvent = new DialogEvent(id);
                break;
            case "enemy":
                int type = int.Parse(node.Attributes["type"].InnerText);
                Debug.Log("Type: " + type);
                int qta = int.Parse(node.Attributes["qta"].InnerText);
                int delay = int.Parse(node.Attributes["delay"].InnerText);
                wEvent = new GenerateEnemyEvent((UFOType)type, qta, delay);
                break;
            case "wait":
                wEvent = new WaitEvent();
                break;
            case "delay":
                int time = int.Parse(node.Attributes["seconds"].InnerText);
                wEvent = new DelayEvent(time);
                break;
            case "powerup":
                //int ptype = int.Parse(node.Attributes["type"].InnerText);
                wEvent = new PowerUpEvent();
                break;
        }

        return wEvent;
    }
    */

    public static void LoadPlayer(Player player)
    {
        //InformationWindow.ShowInformation("Info", "Datapath: " + Application.dataPath + "\n" + "PersistentData: " + Application.persistentDataPath, false);
                
    }

    private static XmlNode GetNode(string id, XmlNodeList nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Attributes["id"].InnerText == id)
            {
                return nodes[i];
            }
        }
        return null;
    }
}
