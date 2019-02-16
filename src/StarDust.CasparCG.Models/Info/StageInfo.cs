﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace StarDust.CasparCG.Models.Info
{

    [XmlRoot(ElementName = "stage")]
    public class StageInfo
    {
        [XmlArray("layers")]
        [XmlArrayItem("layer")]
        public List<LayerInfo> Layers { get; set; }
    }
}
