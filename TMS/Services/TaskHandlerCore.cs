using System;

namespace TMS.Services
{
    public abstract class TaskHandlerCore : IDisposable
    {
        protected readonly ITMSRepository repository;
        protected TaskHandlerCore(ITMSRepository repository)
        {
            this.repository = repository;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    repository.SaveAsync();
                }
                disposedValue = true;
            }
        }

        // Disposing object
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}