using NexusFlow.src.core;
using NexusFlow.src.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusFlow.src.services
{
    internal class OutputCalculator
    {
        /// <summary>
        /// Calculates the data for the given NexusNode.
        /// </summary>
        /// <param name="data">The NexusNodeData to calculate the data for.</param>
        /// <returns>The data calculated for the NexusNode.</returns>
        public static string CalculateData(NexusNodeData data)
        {
            // Check the type of data stored in the NexusNode
            if (data.Type == DataType.Raw)
            {
                // If the data is raw, return it directly
                return data.Data;
            }
            else if (data.Type == DataType.Script)
            {
                // If the data is a script, run the script and return the result
                return ScriptRunner.Run(data.Data).Result;
            }
            else
            {
                // If the data is neither raw nor a script, throw an exception
                throw new Exception("Unsupported NexusNode data type");
            }
        }
    }
}
