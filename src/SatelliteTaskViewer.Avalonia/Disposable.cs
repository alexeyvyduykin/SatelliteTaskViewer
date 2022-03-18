using System;

namespace SatelliteTaskViewer.Avalonia
{
    public abstract class Disposable : IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
            // Подавление финализации
            GC.SuppressFinalize(this);
        }         
    }
}
