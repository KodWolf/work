using System;
using System.Collections.Generic;

namespace PaymentSystem
{
    public interface IPaymentStrategy
    {
        bool Pay(double amount);
        string GetPaymentMethod();
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        private string _cardNumber;
        private string _cardHolder;
        private string _expiryDate;
        private string _cvv;

        public CreditCardPayment(string cardNumber, string cardHolder, string expiryDate, string cvv)
        {
            _cardNumber = cardNumber;
            _cardHolder = cardHolder;
            _expiryDate = expiryDate;
            _cvv = cvv;
        }

        public bool Pay(double amount)
        {
            if (ValidateCard())
            {
                Console.WriteLine($"Оплата картой {MaskCardNumber(_cardNumber)} на сумму {amount:C} выполнена успешно");
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка: недействительные данные карты");
                return false;
            }
        }

        private bool ValidateCard()
        {
            return _cardNumber.Length == 16 && _cvv.Length == 3;
        }

        private string MaskCardNumber(string cardNumber)
        {
            if (cardNumber.Length >= 4)
                return "**** **** **** " + cardNumber.Substring(cardNumber.Length - 4);
            return "****";
        }

        public string GetPaymentMethod()
        {
            return "Банковская карта";
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private string _email;
        private string _password;

        public PayPalPayment(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public bool Pay(double amount)
        {
            if (ValidateAccount())
            {
                Console.WriteLine($"Оплата через PayPal (аккаунт: {_email}) на сумму {amount:C} выполнена успешно");
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка: недействительные данные PayPal");
                return false;
            }
        }

        private bool ValidateAccount()
        {
            return _email.Contains("@") && _password.Length >= 6;
        }

        public string GetPaymentMethod()
        {
            return "PayPal";
        }
    }

    public class CryptoPayment : IPaymentStrategy
    {
        private string _walletAddress;
        private string _currency;

        public CryptoPayment(string walletAddress, string currency)
        {
            _walletAddress = walletAddress;
            _currency = currency.ToUpper();
        }

        public bool Pay(double amount)
        {
            if (ValidateWallet())
            {
                double cryptoAmount = ConvertToCrypto(amount);
                Console.WriteLine($"Оплата криптовалютой {_currency} на сумму {amount:C} (≈ {cryptoAmount:F6} {_currency}) выполнена успешно");
                Console.WriteLine($"Кошелек: {MaskWallet(_walletAddress)}");
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка: недействительный адрес кошелька");
                return false;
            }
        }

        private bool ValidateWallet()
        {
            return _walletAddress.Length >= 26 && _walletAddress.Length <= 42;
        }

        private double ConvertToCrypto(double usdAmount)
        {
            Dictionary<string, double> rates = new Dictionary<string, double>
            {
                { "BTC", 0.000021 },
                { "ETH", 0.00032 },
                { "USDT", 1.0 }
            };

            if (rates.ContainsKey(_currency))
                return usdAmount * rates[_currency];
            return usdAmount * 0.0005;
        }

        private string MaskWallet(string wallet)
        {
            if (wallet.Length >= 8)
                return wallet.Substring(0, 6) + "..." + wallet.Substring(wallet.Length - 4);
            return wallet;
        }

        public string GetPaymentMethod()
        {
            return $"Криптовалюта ({_currency})";
        }
    }

    public class QrCodePayment : IPaymentStrategy
    {
        private string _qrCodeData;
        private string _phoneNumber;

        public QrCodePayment(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            _qrCodeData = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public bool Pay(double amount)
        {
            if (ValidatePhone())
            {
                Console.WriteLine($"Оплата по QR-коду на сумму {amount:C}");
                Console.WriteLine($"QR-код: [{_qrCodeData}]");
                Console.WriteLine($"Подтверждение отправлено на номер {_phoneNumber}");
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка: недействительный номер телефона");
                return false;
            }
        }

        private bool ValidatePhone()
        {
            return _phoneNumber.Length >= 10 && _phoneNumber.Length <= 12;
        }

        public string GetPaymentMethod()
        {
            return "QR-код";
        }
    }

    public class PaymentContext
    {
        private IPaymentStrategy _paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy strategy)
        {
            _paymentStrategy = strategy;
            Console.WriteLine($"Стратегия оплаты изменена на: {strategy.GetPaymentMethod()}");
        }

        public bool ExecutePayment(double amount)
        {
            if (_paymentStrategy == null)
            {
                Console.WriteLine("Ошибка: способ оплаты не выбран");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Ошибка: сумма должна быть больше 0");
                return false;
            }

            Console.WriteLine($"\nОбработка платежа на сумму {amount:C}...");
            return _paymentStrategy.Pay(amount);
        }

        public string GetCurrentPaymentMethod()
        {
            return _paymentStrategy?.GetPaymentMethod() ?? "Не выбран";
        }
    }

    public class PaymentProgram
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("СИСТЕМА ОПЛАТЫ (ПАТТЕРН СТРАТЕГИЯ)");
            Console.WriteLine("====================================");

            var context = new PaymentContext();
            bool running = true;

            while (running)
            {
                try
                {
                    Console.WriteLine("\n--- МЕНЮ ОПЛАТЫ ---");
                    Console.WriteLine("1. Оплатить");
                    Console.WriteLine("2. Выбрать способ оплаты");
                    Console.WriteLine("3. Показать текущий способ");
                    Console.WriteLine("4. Выход");
                    Console.Write("Выберите действие: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            if (context.GetCurrentPaymentMethod() == "Не выбран")
                            {
                                Console.WriteLine("Сначала выберите способ оплаты");
                                break;
                            }

                            Console.Write("Введите сумму для оплаты: ");
                            if (!double.TryParse(Console.ReadLine(), out double amount))
                            {
                                Console.WriteLine("Неверная сумма");
                                break;
                            }

                            context.ExecutePayment(amount);
                            break;

                        case "2":
                            SelectPaymentMethod(context);
                            break;

                        case "3":
                            Console.WriteLine($"Текущий способ: {context.GetCurrentPaymentMethod()}");
                            break;

                        case "4":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Неверный выбор");
                            break;
                    }

                    if (running && choice != "4")
                    {
                        Console.WriteLine("\nНажмите любую клавишу...");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Нажмите любую клавишу...");
                    Console.ReadKey();
                }
            }
        }

        private static void SelectPaymentMethod(PaymentContext context)
        {
            Console.WriteLine("\n--- ВЫБОР СПОСОБА ОПЛАТЫ ---");
            Console.WriteLine("1. Банковская карта");
            Console.WriteLine("2. PayPal");
            Console.WriteLine("3. Криптовалюта");
            Console.WriteLine("4. QR-код");
            Console.Write("Выберите способ: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Номер карты (16 цифр): ");
                    string cardNumber = Console.ReadLine();
                    Console.Write("Владелец карты: ");
                    string cardHolder = Console.ReadLine();
                    Console.Write("Срок действия (ММ/ГГ): ");
                    string expiry = Console.ReadLine();
                    Console.Write("CVV (3 цифры): ");
                    string cvv = Console.ReadLine();

                    context.SetPaymentStrategy(new CreditCardPayment(cardNumber, cardHolder, expiry, cvv));
                    break;

                case "2":
                    Console.Write("Email PayPal: ");
                    string email = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();

                    context.SetPaymentStrategy(new PayPalPayment(email, password));
                    break;

                case "3":
                    Console.Write("Адрес кошелька: ");
                    string wallet = Console.ReadLine();
                    Console.Write("Валюта (BTC/ETH/USDT): ");
                    string currency = Console.ReadLine();

                    context.SetPaymentStrategy(new CryptoPayment(wallet, currency));
                    break;

                case "4":
                    Console.Write("Номер телефона: ");
                    string phone = Console.ReadLine();

                    context.SetPaymentStrategy(new QrCodePayment(phone));
                    break;

                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
}