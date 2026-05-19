using IndependentWork20.Interfaces;

namespace IndependentWork20.Contexts
{
    public class DataContext
    {
        private IDataProcessorStrategy _strategy;

        public DataContext(IDataProcessorStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IDataProcessorStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecuteProcessing(string data)
        {
            _strategy.Process(data);
        }
    }
}