using System;
using System.Collections.Generic;
using System.Text;
using GlmSharp;

namespace JsonLoaderSample.DataProvider
{
    public interface IDataProviderPass
    {
        void Calculate(double t);

        void SetTime(DateTime begin, TimeSpan span);
      
        dmat4 ModelMatrix { get; }
    }


    public interface IDataProvider : IDataProviderPass
    {
        dvec3 Position { get; }
    }

    public interface IDataProviderCollection : IDataProviderPass
    {
        dvec3Arr Positions { get; }
    }

    public abstract class DataProviderBase : IDataProvider
    {
        public dvec3 Position { get; } = dvec3.NaN;

        public dmat4 ModelMatrix { get; } = dmat4.AllNaN;

        public abstract void SetTime(DateTime begin, TimeSpan span);

        public abstract void Calculate(double t);
    }

    public class dvec3Arr
    {
        public dvec3 this[string name]
        {
            get
            {
                return dvec3.Epsilon;
            }
        }
    }
}
