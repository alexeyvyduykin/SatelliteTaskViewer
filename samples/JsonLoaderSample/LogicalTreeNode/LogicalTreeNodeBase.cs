using System;
using System.Collections.Generic;
using System.Text;
using GlmSharp;
using JsonLoaderSample.DataProvider;

namespace JsonLoaderSample.LogicalTreeNode
{
    public abstract class LogicalTreeNodeBase : ILogicalTreeNode
    {
        public LogicalTreeNodeBase(string name)
        {
            this.Name = name;
        }

        public ILogicalTreeNode? Parent { get; set; }

        public IList<ILogicalTreeNode> Children { get; set; } = new List<ILogicalTreeNode>();

        public string Name { get; set; } = string.Empty;

        public IDataProviderPass? DataProvider { get; protected set; }

        public DateTime DateTimeBegin { get; set; }

        public TimeSpan TimeSpan { get; set; }

        protected abstract ILogicalTreeNode Self();

        public dmat4 ModelMatrix
        {
            get
            {
                if (DataProvider == null)
                {
                    return dmat4.Zero;
                }

                if (DataProvider.ModelMatrix == dmat4.AllNaN)
                {
                    if (Parent == null)
                    {
                        return dmat4.Zero;
                    }

                    return Parent.ModelMatrix;
                }

                return DataProvider.ModelMatrix;
            }
        }

        public ILogicalTreeNode AddChild(ILogicalTreeNode node)
        {
            node.Parent = Self();

            if (Children == null)
                Children = new List<ILogicalTreeNode>();

            Children.Add(node);

            return node;
        }

        public ILogicalTreeNode? Find(Func<ILogicalTreeNode, bool> equal)
        {
            if (equal.Invoke(Self()) == true)
                return Self();

            foreach (var item in Children)
            {
                var res = item.Find(equal);

                if (res != null)
                {
                    return res;
                }
            }

            return null;
        }

        public void Calculate(double t)
        {         
            if(DataProvider != null)
            {
                DataProvider.Calculate(t);
            }
           
            foreach (var node in Children)
            {
                node.Calculate(t);
            }
        }

        public void SetTime(DateTime begin, TimeSpan span)
        {
            this.DateTimeBegin = begin;
            this.TimeSpan = span;
                          
            if(DataProvider != null)
            {
                DataProvider.SetTime(begin, span);
            }

            OnTimeChanged?.Invoke(this, new TimeSynchronizerEventArgs() { DateTimeBegin = begin, TimeSpan = span });

            foreach (var node in Children)
            {
                node.SetTime(begin, span);
            }
        }

        public event TimeSynchronizerChanged? OnTimeChanged;

        public ILogicalTreeNode Root
        {
            get
            {
                var node = Self();
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return node;
            }
        }

        public string FullName
        {
            get
            {
                if (Parent == null)
                    return Name;

                return string.Format("{0}.{1}", Parent.FullName, Name);
            }
        }


    }

    public class TimeSynchronizerEventArgs
    {
        public DateTime DateTimeBegin { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }

    public delegate void TimeSynchronizerChanged(object sender, TimeSynchronizerEventArgs args);

    //public class TimeSynchronizer
    //{
    //    public event TimeSynchronizerChanged OnTimeChanged;

    //    public DateTime DateTimeBegin { get; set; }

    //    public TimeSpan TimeSpan { get; set; }

    //    public void SetTime(DateTime begin, TimeSpan span)
    //    {
    //        this.DateTimeBegin = begin;
    //        this.TimeSpan = span;

    //        OnTimeChanged?.Invoke(this, new TimeSynchronizerEventArgs() { DateTimeBegin = begin, TimeSpan = span });
    //    }
    //}
}
