namespace Northwind.Fulfillment;

public sealed record FulfillmentRequest(
  int ProductId,
  int Quantity,
  int UnitsInStock,
  bool AllowBackOrder);

public sealed record ReadyToShip(
  int ProductId,
  int Quantity)
{
  public string Status => "readyToShip";
}

public sealed record BackOrdered(
  int ProductId,
  int Quantity,
  int UnitsAvailable,
  int EstimatedDays)
{
  public string Status => "backOrdered";
}

public sealed record UnableToFulfill(
  int ProductId,
  int Quantity,
  int UnitsAvailable,
  string Reason)
{
  public string Status => "unableToFulfill";
}

public union FulfillmentDecision(
  ReadyToShip,
  BackOrdered,
  UnableToFulfill);

public sealed class FulfillmentService
{
  public FulfillmentDecision Decide(
    FulfillmentRequest request)
  {
    if (request.Quantity <= request.UnitsInStock)
    {
      return new ReadyToShip(
        ProductId: request.ProductId,
        Quantity: request.Quantity);
    }

    if (request.AllowBackOrder)
    {
      return new BackOrdered(
        ProductId: request.ProductId,
        Quantity: request.Quantity,
        UnitsAvailable: request.UnitsInStock,
        EstimatedDays: 7);
    }

    return new UnableToFulfill(
      ProductId: request.ProductId,
      Quantity: request.Quantity,
      UnitsAvailable: request.UnitsInStock,
      Reason: "Insufficient stock and back orders are disabled.");
  }
}
