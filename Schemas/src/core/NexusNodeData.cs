using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexusFlow.src.services;

namespace NexusFlow.src.core
{
    public enum DataType
    {
        Raw,
        Script
    }
    public class NexusNodeData
    {
        public DataType Type { get; set; }
        public string Data { get; set; }

        public NexusNodeData(DataType type, string data)
        {
            Type = type;
            Data = data;
        }
        public string Result()
        {
            return OutputCalculator.CalculateData(this);
        }
    }

}
