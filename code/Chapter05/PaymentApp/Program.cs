using System.Globalization; // To use CultureInfo.
using Packt.Shared; // To use PaymentProcessor and related types.

CultureInfo.CurrentCulture =
  CultureInfo.GetCultureInfo("en-US");

PaymentProcessor processor = new();

PaymentRequest[] requests =
[
  new(
    TransactionId: "TX-1001",
    Amount: 49.99M,
    AvailableFunds: 100M,
    RequiresReview: false),

  new(
    TransactionId: "TX-1002",
    Amount: 125M,
    AvailableFunds: 80M,
    RequiresReview: false),

  new(
    TransactionId: "TX-1003",
    Amount: 75M,
    AvailableFunds: 100M,
    RequiresReview: true)
];

foreach (PaymentRequest request in requests)
{
  PaymentOutcome outcome = processor.Process(request);

  string message = outcome switch
  {
    PaymentApproved(var transactionId, var amount) =>
      $"Approved {transactionId} for {amount:C}.",

    PaymentDeclined(var reason, var declineCode) =>
      $"Declined: {reason} ({declineCode}).",

    PaymentPending(var providerReference, var retryAfter) =>
      $"Pending {providerReference}; retry after " +
      $"{retryAfter.TotalMinutes:N0} minutes."
  };

  Console.WriteLine(message);
}
