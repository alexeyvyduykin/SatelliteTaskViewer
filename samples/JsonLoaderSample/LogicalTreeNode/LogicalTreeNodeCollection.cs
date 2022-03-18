using System;
using System.Collections.Generic;
using System.Text;
using GlmSharp;
using JsonLoaderSample.DataProvider;

namespace JsonLoaderSample.LogicalTreeNode
{
    public class LogicalTreeNodeCollection : LogicalTreeNodeBase, ILogicalTreeNode
    {
        protected override ILogicalTreeNode Self()
        {
            return this;
        }

       // public IDictionary<string, IDataProvider> DataProviderCollection { get; } = new Dictionary<string, IDataProvider>();

        public LogicalTreeNodeCollection(string name, IDataProviderCollection dataProviderCollection) : base(name)
        {
            this.DataProvider = dataProviderCollection;
        }
    }



}
