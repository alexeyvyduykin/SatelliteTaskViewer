using System;
using System.Collections.Generic;
using System.Text;
using GlmSharp;

namespace JsonLoaderSample.LogicalTreeNode
{
    public interface ILogicalTreeNode
    {
        ILogicalTreeNode? Parent { get; set; }

        IList<ILogicalTreeNode> Children { get; set; }

        string Name { get; set; }

        string FullName { get; }

        dmat4 ModelMatrix { get; }

        DateTime DateTimeBegin { get; set; }

        TimeSpan TimeSpan { get; set; }

        event TimeSynchronizerChanged? OnTimeChanged;

        ILogicalTreeNode AddChild(ILogicalTreeNode node);

        ILogicalTreeNode? Find(Func<ILogicalTreeNode, bool> equal);

        void Calculate(double t);

        void SetTime(DateTime begin, TimeSpan span);
    
    }

}
