using System;
using System.Collections.Generic;
using System.Text;
using GlmSharp;
using JsonLoaderSample.DataProvider;

namespace JsonLoaderSample.LogicalTreeNode
{
    public class LogicalTreeNode : LogicalTreeNodeBase, ILogicalTreeNode
    {
        protected override ILogicalTreeNode Self()
        {
            return this;
        }

        //public dvec3 Position
        //{
        //    get
        //    {
        //        var node = this;
        //        while (node.DataProvider.Position == dvec3.NaN)
        //        {
        //            node = node.Parent;
        //        }

        //        return node.DataProvider.Position;
        //    }
        //}
   
        public LogicalTreeNode(string name, IDataProvider dataProvider) : base(name)
        {
            this.DataProvider = dataProvider;
        }
    }

}
