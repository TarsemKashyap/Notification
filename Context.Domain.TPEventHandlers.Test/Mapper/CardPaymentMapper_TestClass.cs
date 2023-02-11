using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.TPEventHandlers.Mapper;
using Example.Notific.TPF.WebApi.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.TPEventHandlers.Test.Mapper
{
    [TestClass]
    public class CardPaymentMapper_TestClass
    {
        [TestMethod]
        public void CardPaymentMapper_ToResource()
        {
            TPCardPayment payment = new TPCardPayment()
            {
                CallerId = "CalledID",
                CallerReference = "CallerReference",
                InitiatedBy = "By",
                IpAddress = "192.168.1.1",
                Number = "123456789",
                Parent = "Parent",
                Receipt = "TestName",
                Service = 1,
                Status = 2,
                Type = 1
            };

            Transaction transaction = new Transaction()
            {
                Amount = 15.0M,
                AuthCode = "12345",
                Currency = "NZD",
                Date = DateTime.Now,
                Particulars = "TShirt",
                ProviderResponse = "500",
                Reference = "Test",
                ResponseCode = 200,
                ResponseMessage = "Response",
                SettlementDate = DateTime.Now.ToString()
            };

            payment.Transaction = transaction;

            Card card = new Card()
            {
                CardNumber = "123456******7890",
                CardScheme = "Visa",
                ExpiryDate = "0215",
                MapToken = "123456",
                NameOnCard = "Jitender",
                CardBin = "123456",
                CardLastFour = "7890",
                CardTypeId = 1
            };

            payment.Card = card;

            Device device = new Device()
            {
                Description = "Description",
                Id = "12345"
            };

            payment.Device = device;

            Geolocation location = new Geolocation()
            {
                Latitude = "565.56",
                Longitude = "565.55"
            };

            payment.Geolocation = location;

            Merchant merchant = new Merchant()
            {
                Id = 20123,
                SubAccountId = 90909
            };

            payment.Merchant = merchant;

            var actual = CardPaymentMapper.ToResource(payment);

            Assert.AreEqual(actual.Amount, payment.Transaction.Amount, "Amount values are not same");
            Assert.AreEqual(actual.Channel, Dictionaries.Channel[payment.Service], "Service channel values are not same");
            Assert.AreEqual(actual.Currency, payment.Transaction.Currency, "Currency values are not same");
            Assert.AreEqual(actual.InitiatedBy, payment.InitiatedBy, "InitiatedBy values are not same");
            Assert.AreEqual(actual.Number, payment.Number, "Transaction number values are not same");
            Assert.AreEqual(actual.Particulars, payment.Transaction.Particulars, "Particulars values are not same");
            Assert.AreEqual(actual.ReceiptRecipient, payment.ReceiptRecipient, "ReceiptRecipient values are not same");
            Assert.AreEqual(actual.Reference, payment.Transaction.Reference, "Reference values are not same");
            Assert.AreEqual(actual.Status, (Enum.GetName(typeof(PaymentStatus), payment.Status)).ToLower(), "Payment status values are not same");
            Assert.AreEqual(actual.Timestamp, payment.Transaction.Date, "Timestamp values are not same");
            Assert.AreEqual(actual.Type, (Enum.GetName(typeof(PaymentType), payment.Type)).ToLower(), "Payment type values are not same");
            Assert.AreEqual(actual.Response.AuthCode, payment.Transaction.AuthCode, "AuthCode values are not same");
            Assert.AreEqual(actual.Response.Code, payment.Transaction.ResponseCode, "ResponseCode values are not same");
            Assert.AreEqual(actual.Response.Message, payment.Transaction.ResponseMessage, "Message values are not same");
            Assert.AreEqual(actual.Response.ProviderResponse, payment.Transaction.ProviderResponse, "ProviderResponse values are not same");
            Assert.AreEqual(actual.PaymentMethod.Card.Bin, payment.Card.CardBin, "Card bin values are not same");
            Assert.AreEqual(actual.PaymentMethod.Card.ExpiryDate, payment.Card.ExpiryDate, "Card expiry date values are not same");
            Assert.AreEqual(actual.PaymentMethod.Card.LastFour, payment.Card.CardLastFour, "Card last  value four are not same");
            Assert.AreEqual(actual.PaymentMethod.Card.Mask, payment.Card.CardNumber, "Card number values are not same");
            Assert.AreEqual(actual.PaymentMethod.Card.Token, payment.Card.MapToken, "Card token values are not same");
            Assert.AreEqual(actual.PaymentMethod.Card.Type, (Enum.GetName(typeof(CardType), payment.Card.CardTypeId)).ToLower(), "Card type values are not same");
            Assert.AreEqual(actual.Merchant.Id, payment.Merchant.Id, "Merchant id values are not same");
            Assert.AreEqual(actual.Merchant.SubAccountId, payment.Merchant.SubAccountId, "Merchant subaccount id values are not same");
            Assert.AreEqual(actual.Device.Id, payment.Device.Id, "Device id values are not same");
            Assert.AreEqual(actual.Device.Description, payment.Device.Description, "Device description values are not same");
            Assert.AreEqual(actual.Geolocation.Latitude, payment.Geolocation.Latitude, "Location latitude  values are not same");
            Assert.AreEqual(actual.Geolocation.Longitude, payment.Geolocation.Longitude, "Location longitude values are not same");
        }

        [TestMethod]
        public void CardPaymentMapper_ToResource_Device_Null()
        {
            TPCardPayment payment = new TPCardPayment()
            {
                CallerId = "CalledID",
                CallerReference = "CallerReference",
                InitiatedBy = "By",
                IpAddress = "192.168.1.1",
                Number = "123456789",
                Parent = "Parent",
                Receipt = "TestName",
                Service = 1,
                Status = 2,
                Type = 1
            };

            Transaction transaction = new Transaction()
            {
                Amount = 15.0M,
                AuthCode = "12345",
                Currency = "NZD",
                Date = DateTime.Now,
                Particulars = "TShirt",
                ProviderResponse = "500",
                Reference = "Test",
                ResponseCode = 200,
                ResponseMessage = "Response",
                SettlementDate = DateTime.Now.ToString()
            };

            payment.Transaction = transaction;

            Card card = new Card()
            {
                CardNumber = "123456******7890",
                CardScheme = "Visa",
                ExpiryDate = "0215",
                MapToken = "123456",
                NameOnCard = "Jitender"
            };

            payment.Card = card;

            payment.Device = null;

            Geolocation location = new Geolocation()
            {
                Latitude = "565.56",
                Longitude = "565.55"
            };

            payment.Geolocation = location;

            Merchant merchant = new Merchant()
            {
                Id = 20123,
                SubAccountId = 90909
            };

            payment.Merchant = merchant;

            var actual = CardPaymentMapper.ToResource(payment);

            Assert.IsNull(actual.Device, "The device should be null");
        }

        [TestMethod]
        public void CardPaymentMapper_ToResource_GeoLocation_Null()
        {
            TPCardPayment payment = new TPCardPayment()
            {
                CallerId = "CalledID",
                CallerReference = "CallerReference",
                InitiatedBy = "By",
                IpAddress = "192.168.1.1",
                Number = "123456789",
                Parent = "Parent",
                Receipt = "TestName",
                Service = 1,
                Status = 2,
                Type = 1
            };

            Transaction transaction = new Transaction()
            {
                Amount = 15.0M,
                AuthCode = "12345",
                Currency = "NZD",
                Date = DateTime.Now,
                Particulars = "TShirt",
                ProviderResponse = "500",
                Reference = "Test",
                ResponseCode = 200,
                ResponseMessage = "Response",
                SettlementDate = DateTime.Now.ToString()
            };

            payment.Transaction = transaction;

            Card card = new Card()
            {
                CardNumber = "123456******7890",
                CardScheme = "Visa",
                ExpiryDate = "0215",
                MapToken = "123456",
                NameOnCard = "Jitender"
            };

            payment.Card = card;

            payment.Device = null;

            payment.Geolocation = null;

            Merchant merchant = new Merchant()
            {
                Id = 20123,
                SubAccountId = 90909
            };

            payment.Merchant = merchant;

            var actual = CardPaymentMapper.ToResource(payment);

            Assert.IsNull(actual.Geolocation, "The geolocation should be null");
        }
    }
}
