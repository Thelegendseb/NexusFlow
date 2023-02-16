using System;
using System.Collections.Generic;
using NexusFlow.src.models.DB;

namespace NexusFlow.src.core
{
    public class NexusNode
    {
        /// <summary>
        /// The ID of the parent node of the node.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// The Id of the node.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The name of the node.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The data stored by the node.
        /// </summary>
        public NexusNodeData Data { get; private set; }

        /// <summary>
        /// The IDs of the child nodes of the node.
        /// </summary>
        public List<string> ChildrenIds { get; private set; }

        /// <summary>
        /// Creates a new NexusNode instance with the specified name and data.
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <param name="data">The data stored by the node.</param>

        public NexusNode(string name, NexusNodeData data)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Data = data;
            ChildrenIds = new List<string>();
        }

        /// <summary>
        /// Converts a `NexusNodeDB` object to a `NexusNode` object.
        /// </summary>
        /// <param name="dbNode">The `NexusNodeDB` object to be converted.</param>
        /// <returns>The converted `NexusNode` object.</returns>
        public static NexusNode FromDb(NexusNodeDB dbNode)
        {
            NexusNodeData data = new NexusNodeData((DataType)dbNode.DataType, dbNode.Data);
            NexusNode node = new NexusNode(dbNode.Name, data);
            node.ParentId = dbNode.ParentId;
            node.ChildrenIds = dbNode.ChildrenIds;
            return node;
        }

        /// <summary>
        /// Converts a `NexusNode` object to a `NexusNodeDB` object.
        /// </summary>
        /// <param name="node">The `NexusNode` object to be converted.</param>
        /// <returns>The converted `NexusNodeDB` object.</returns>
        public static NexusNodeDB ToDb(NexusNode node)
        {
            NexusNodeDB dbNode = new NexusNodeDB();
            dbNode.Id = node.Id;
            dbNode.ParentId = node.ParentId;
            dbNode.Name = node.Name;
            dbNode.DataType = (int)node.Data.Type;
            dbNode.Data = node.Data.Data;
            dbNode.ChildrenIds = node.ChildrenIds;
            return dbNode;
        }

    }
}
