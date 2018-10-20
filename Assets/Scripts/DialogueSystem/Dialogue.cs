using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Dialogue {

    public List<DialogueNode> Nodes;

    public Dialogue()
    {
        Nodes = new List<DialogueNode>();
    }

    public static Dialogue LoadDialogue(string path)
    {
        XmlSerializer serz = new XmlSerializer(typeof(Dialogue));
        StreamReader reader = new StreamReader(path);

        Dialogue dia = (Dialogue)serz.Deserialize(reader);

        return dia;
    }

}
