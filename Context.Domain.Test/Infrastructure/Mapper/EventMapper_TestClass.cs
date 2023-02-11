using Example.MAP.Common.API.Contract.Resources.V1;
using Example.Notific.Context.Common.Helpers;
using Example.Notific.Context.Domain.Infrastructure.Mapper;
using Example.Notific.Context.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Infrastructure.Mapper
{
    [TestClass]
    public class EventMapper_TestClass
    {
        [TestMethod]
        public void EventMapper_CardPayment_ToResource()
        {
            Payment payment = new Payment()
            {
                InitiatedBy = "By",
                Number = "123456789",
                Amount = 15.0M,
                Channel = "ivr",
                Currency = "NZD",
                Particulars = "Test",
                ReceiptRecipient = "Receipt",
                Reference = "Reference",
                Timestamp = DateTime.Now,
                Status = "successful",
                Type = "payment"
            };

            Response response = new Response()
            {
                AuthCode = "1234",
                Code = 123,
                Message = "Message",
                ProviderResponse = "Response"
            };

            payment.Response = response;

            PaymentCardDetails card = new PaymentCardDetails()
            {
                ExpiryDate = "0215",
                NameOnCard = "Jitender",
                Bin = "1234",
                LastFour = "5678",
                Mask = "123456******7890",
                Token = "123546",
                Type = "Visa"
            };

            PaymentMethod method = new PaymentMethod()
            {
                Card = card
            };

            payment.PaymentMethod = method;

            PaymentDevice device = new PaymentDevice()
            {
                Description = "Description",
                Id = "12345"
            };

            payment.Device = device;

            PaymentGeolocation location = new PaymentGeolocation()
            {
                Latitude = "565.56",
                Longitude = "565.55"
            };

            payment.Geolocation = location;

            PaymentMerchant merchant = new PaymentMerchant()
            {
                Id = 20123,
                SubAccountId = 90909
            };

            payment.Merchant = merchant;

            var eventModel = new Domain.Model.Event(Common.EventType.PgPaymentSuccessful, DateTime.Now, 20001, "Content", "CreatedBy", Common.ContentType.Payment,"Secret");

            var actual = EventMapper.ToResource(payment, eventModel);

            //Assert.AreEqual((decimal)actual.DataElement.Content.Amount, payment.Amount, "Amount values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Channel, payment.Channel, "Service channel values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Currency, payment.Currency, "Currency values are not same");
            //Assert.AreEqual(actual.DataElement.Content.InitiatedBy, payment.InitiatedBy, "InitiatedBy values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Number, payment.Number, "Transaction number values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Particulars, payment.Particulars, "Particulars values are not same");
            //Assert.AreEqual(actual.DataElement.Content.ReceiptRecipient, payment.ReceiptRecipient, "ReceiptRecipient values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Reference, payment.Reference, "Reference values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Status, payment.Status, "Payment status values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Timestamp, payment.Timestamp, "Timestamp values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Type, payment.Type, "Payment type values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Response.AuthCode, payment.Response.AuthCode, "AuthCode values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Response.Code, payment.Response.Code, "ResponseCode values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Response.Message, payment.Response.Message, "Message values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Response.ProviderResponse, payment.Response.ProviderResponse, "ProviderResponse values are not same");
            //Assert.AreEqual(actual.DataElement.Content.PaymentMethod.Card.Bin, payment.PaymentMethod.Card.Bin, "Card bin values are not same");
            //Assert.AreEqual(actual.DataElement.Content.PaymentMethod.Card.ExpiryDate, payment.PaymentMethod.Card.ExpiryDate, "Card expiry date values are not same");
            //Assert.AreEqual(actual.DataElement.Content.PaymentMethod.Card.LastFour, payment.PaymentMethod.Card.LastFour, "Card last  value four are not same");
            //Assert.AreEqual(actual.DataElement.Content.PaymentMethod.Card.Mask, payment.PaymentMethod.Card.Mask, "Card number values are not same");
            //Assert.AreEqual(actual.DataElement.Content.PaymentMethod.Card.Token, payment.PaymentMethod.Card.Token, "Card token values are not same");
            //Assert.AreEqual(actual.DataElement.Content.PaymentMethod.Card.Type, payment.PaymentMethod.Card.Type, "Card type values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Merchant.Id, payment.Merchant.Id, "Merchant id values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Merchant.SubAccountId, payment.Merchant.SubAccountId, "Merchant subaccount id values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Device.Id, payment.Device.Id, "Device id values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Device.Description, payment.Device.Description, "Device description values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Geolocation.Latitude, payment.Geolocation.Latitude, "Location latitude  values are not same");
            //Assert.AreEqual(actual.DataElement.Content.Geolocation.Longitude, payment.Geolocation.Longitude, "Location longitude values are not same");

            //Assert.AreEqual(actual.Received, eventModel.Received.ToString("s") + "Z", "Received date values are not same");
            //Assert.AreEqual(actual.Type, EnumExtensions.GetString(eventModel.Type), "Event type values are not same");
        }
    }
}
