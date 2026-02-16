public interface INumericOperationStrategy
{
    double Execute(double value);
    string OperationName { get; }
}
