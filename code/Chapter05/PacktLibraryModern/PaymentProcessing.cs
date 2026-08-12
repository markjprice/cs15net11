namespace Packt.Shared;

public record class PaymentRequest(
  string TransactionId,
  decimal Amount,
  decimal AvailableFunds,
  bool RequiresReview);

public record class PaymentApproved(
  string TransactionId,
  decimal Amount);

public record class PaymentDeclined(
  string Reason,
  string DeclineCode);

public record class PaymentPending(
  string ProviderReference,
  TimeSpan RetryAfter);

public union PaymentOutcome(
  PaymentApproved,
  PaymentDeclined,
  PaymentPending);

public class PaymentProcessor
{
  public PaymentOutcome Process(PaymentRequest request)
  {
    if (request.Amount <= 0)
    {
      return new PaymentDeclined(
        Reason: "The amount must be greater than zero",
        DeclineCode: "invalid_amount");
    }

    if (request.Amount > request.AvailableFunds)
    {
      return new PaymentDeclined(
        Reason: "Insufficient funds",
        DeclineCode: "card_declined");
    }

    if (request.RequiresReview)
    {
      return new PaymentPending(
        ProviderReference: $"REVIEW-{request.TransactionId}",
        RetryAfter: TimeSpan.FromMinutes(5));
    }

    return new PaymentApproved(
      TransactionId: request.TransactionId,
      Amount: request.Amount);
  }
}
