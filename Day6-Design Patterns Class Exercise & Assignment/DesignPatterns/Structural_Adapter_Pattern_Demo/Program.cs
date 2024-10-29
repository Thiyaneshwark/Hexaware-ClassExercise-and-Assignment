using Structural_Adapter_Pattern_Demo;

OldPaymentService oldPaymentService = new OldPaymentService();

IPaymentProcessor paymentProcessor = new PaymentAdapter(oldPaymentService);
Client client = new Client(paymentProcessor);
client.MakePayment(89988.99M);
Console.ReadLine();