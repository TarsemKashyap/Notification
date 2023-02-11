using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Common
{
    /// <summary>
    /// Delivery methods
    /// </summary>
    public enum DeliveryMethod
    {
        [Description("email")]
        Email = 1,
        [Description("httpPost")]
        HttpPost = 2
    }

    /// <summary>
    /// Status of notification
    /// </summary>
    public enum NotificationStatus
    {
        NotDelivered = 1,
        Delivered = 2,
        Failed = 3
    }

    /// <summary>
    /// TP framework and Domain events type
    /// </summary>
    public enum EventType
    {
        [Description("payment.successful")]
        PgPaymentSuccessful = 1,

        [Description("payment.failed")]
        PgPaymentFailed = 2,

        [Description("payment.refunded")]
        PgPaymentRefunded = 3,

        [Description("payment.captured")]
        PgPaymentCaptured = 4,

        [Description("directdebit.successful")]
        DdPaymentSuccessful = 5,

        [Description("directdebit.dishonoured")]
        DdPaymentDishonoured = 6,

        [Description("cardplan.created")]
        RpCardPlanCreated = 7,

        [Description("cardplan.updated")]
        RpCardPlanUpdated = 8,

        [Description("cardplan.suspended")]
        RpCardPlanSuspended = 9,

        [Description("cardplan.resumed")]
        RpCardPlanResumed = 10,

        [Description("cardplan.ended")]
        RpCardPlanEnded = 11,

        [Description("cardplan.cancelled")]
        RpCardPlanCancelled = 12,

        [Description("directdebitplan.created")]
        RpDdPlanCreated = 13,

        [Description("directdebitplan.updated")]
        RpDdPlanUpdated = 14,

        [Description("directdebitplan.approved")]
        RpDdPlanApproved = 15,

        [Description("directdebitplan.suspended")]
        RpDdPlanSuspended = 16,

        [Description("directdebitplan.resumed")]
        RpDdPlanResumed = 17,

        [Description("directdebitplan.ended")]
        RpDdPlanEnded = 18,

        [Description("directdebitplan.cancelled")]
        RpDdPlanCancelled = 19,

        [Description("directdebitplan.rejected")]
        RpDdPlanRejected = 20,

        [Description("cardplan.payment")]
        RpCardPlanPaymentProcessed = 21,

        [Description("directdebitplan.payment")]
        RpDdPlanPaymentProcessed = 22,

        [Description("polipayment.complete")]
        PgPoliPaymentComplete = 23



    }

    /// <summary>
    /// Content type of events
    /// </summary>
    public enum ContentType
    {
        Payment = 1,
        DirectDebit = 2,
        CardPlan = 3,
        DirectDebitPlan = 4
    }

    /// <summary>
    /// Payment types
    /// </summary>
    public enum PaymentType
    {
        Purchase = 1,
        Refund = 2,
        Capture = 3,
        Authorise = 4,
        POLi = 5,
        Authentication = 10
    }

    /// <summary>
    /// Payment status
    /// </summary>
    public enum PaymentStatus
    {
        Unknown = 0,
        Successful = 2,
        Failed = 3,
        Blocked = 4,
        Declined = 11
    }

    /// <summary>
    /// Card types
    /// </summary>
    public enum CardType
    {
        Amex = 1,
        BankCard = 2,
        Diners = 3,
        MasterCard = 4,
        Visa = 5,
        UnionPay = 6
    }

    /// <summary>
    /// Direct debit status
    /// </summary>
    public enum DDStatus
    {
        Scheduled = 1,
        Processing = 2,
        Successful = 3,
        Dishonoured = 4,
        Failed = 5,
        Cancelled = 7
    }

    public enum PlanScheduleStatus
    {
        Scheduled = 1,
        Processed = 2
    }

    public enum CardPlanType
    {
        [Description("one-off")]
        OneOff = 1,

        [Description("per-invoice")]
        PerInvoice = 2,

        [Description("recurring")]
        Recurring = 3,

        [Description("instalment")]
        Instalment = 4
    }

    public enum CardPlanStatus
    {
        Active = 1,
        Cancelled = 2,
        Ended = 3,
        Suspended = 4
    }

    public enum CardPlanFrequency
    {
        [Description("daily")]
        Daily = 1,

        [Description("weekly")]
        Weekly = 2,

        [Description("fortnightly")]
        Fortnightly = 3,

        [Description("4-weekly")]
        FourWeekly = 4,

        [Description("8-weekly")]
        EightWeekly = 5,

        [Description("12-weekly")]
        TweleveWeekly = 6,

        [Description("monthly")]
        Monthly = 7,

        [Description("monthly-last-buisness-day")]
        MonthlyLast = 8,

        [Description("monthly-specific-week")]
        MonthlySpecificWeek = 9,

        [Description("2-monthly")]
        TwoMonthly = 10,

        [Description("3-monthly")]
        ThreeMonthly = 11,

        [Description("6-monthly")]
        SixMonthly = 12,

        [Description("12-monthly")]
        TweleveMonthly = 13,

        [Description("one-off-payment")]
        OneOffPayment = 14,

        [Description("per-invoice")]
        PerInvoice = 15
    }

    public enum CardPlanInstalementOptions
    {
        [Description("none")]
        None = 1,

        [Description("add-to-next")]
        AddToNext = 2,

        [Description("add-to-last")]
        AddToLast = 3,

        [Description("add-at-end")]
        AddAtEnd = 4

    }

    public enum CardPlanAmendmentType
    {
        Status = 1,
        Plan = 2,
        Card = 5,
        Payer = 6,
        PaymentDate = 3,
        AddPaymentDate = 4
    }

    public enum DDPlanType
    {
        [Description("one-off")]
        OneOff = 1,

        [Description("per-invoice")]
        PerInvoice = 2,

        [Description("recurring")]
        Recurring = 3,

        [Description("instalment")]
        Instalment = 4
    }

    public enum DDPlanFrequency
    {
        [Description("daily")]
        Daily = 1,

        [Description("weekly")]
        Weekly = 2,

        [Description("fortnightly")]
        Fortnightly = 3,

        [Description("4-weekly")]
        FourWeekly = 4,

        [Description("8-weekly")]
        EightWeekly = 5,

        [Description("12-weekly")]
        TweleveWeekly = 6,

        [Description("monthly")]
        Monthly = 7,

        [Description("monthly-last-buisness-day")]
        MonthlyLast = 8,

        [Description("2-monthly")]
        TwoMonthly = 10,

        [Description("3-monthly")]
        ThreeMonthly = 11,

        [Description("6-monthly")]
        SixMonthly = 12,

        [Description("12-monthly")]
        TweleveMonthly = 13,

        [Description("one-off-payment")]
        OneOffPayment = 14,

        [Description("per-invoice")]
        PerInvoice = 15
    }

    public enum DDPlanInstalementOptions
    {
        [Description("none")]
        None = 1,

        [Description("add-to-next")]
        AddToNext = 2,

        [Description("add-to-last")]
        AddToLast = 3,

        [Description("add-at-end")]
        AddAtEnd = 4

    }

    public enum DDPlanAmendmentType
    {
        Status = 1,
        Plan = 2,
        Card = 5,
        Payer = 6,
        PaymentDate = 3,
        AddPaymentDate = 4
    }

    public enum DirectDebitPlanStatus
    {
        PendingApproval = 1,
        PendingAmendmentApproval = 2,
        PendingCancellationApproval = 3,
        Active = 4,
        Suspended = 5,
        Ended = 6,
        Cancelled = 7,
        ActiveWithAmendment = 8,
        EndedDishonor = 9,
        SuspendingNewAuthority = 10,
        ActiveWithDishonor = 11,
        PendingEmailVerification = 12,
        PendingVoiceVerification = 13,
        Rejected = 14
    }

    /// <summary>
    /// Transaction Typs
    /// </summary>
    public enum TransactionType
    {
        Purchase = 1,
        Refund = 2,
        Capture = 3,
        Authorisation = 4,
        POLi = 5,
        Authentication = 10
    }

    /// <summary>
    /// Transaction Status
    /// </summary>
    public enum PaymentTransactionStatus
    {
        Unknown = 0,
        Successful = 2,
        Failed = 3,
        Blocked = 4,
        NotComplete = 10,
        Declined = 11,
        ThreedsNotComplete = 14
    }

    public enum PaymentService
    {
        StandardIVR = 1,            // Phone2Pay
        VirtualTerminal = 2,
        WebPaymentLite = 3,
        StandardWebIntegration = 4, // Web2PayStandard
        [Obsolete("Web2PayBillPayment is obsolete - not defined in this context.", true)]
        Web2PayBillPayment = 5,
        ShoppingCartWebIntegration = 6,
        [Obsolete("DebitToPay is obsolete - not defined in this context.", true)]
        Debit2Pay = 7,
        StandardBatchPayments = 8,
        [Obsolete("AccountReconciliation is obsolete - not defined in this context.", true)]
        AccountReconciliation = 9,
        [Obsolete("Speech2Pay is obsolete - not defined in this context.", true)]
        Speech2Pay = 10,
        WebPayment3Party = 11,
        WebPaymentAPI = 12,         // Web2PayWebService
        MobileApplication = 13,
        StandardRecurringPayments = 14,
        [Obsolete("MerchantSite is obsolete - not defined in this context.", true)]
        MerchantSite = 15,
        CardTokenManagement = 16,   // 1$ Auth legacy
        [Obsolete("MerchantFundDistribution is obsolete - not defined in this context.", true)]
        MerchantFundDistribution = 17,
        [Obsolete("P2PPaymentsWebService is obsolete - not defined in this context.", true)]
        P2PPaymentsWebService = 18
    }
    public enum PoliPaymentStatus
    {
        Unknown = 0,
        Successful = 2,
        Failed = 3,
        NotComplete = 10
    }
    public enum VerificationMethod
    {
        SecretOnly = 1,
        Hmac = 2
    }
}
