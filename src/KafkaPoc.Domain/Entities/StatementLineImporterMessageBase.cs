using System.Runtime.Serialization;

namespace KafkaPoc.Domain.Entities;

public class StatementLineImporterMessageBase
{
    public int ClientFundLineId { get; set; }
    public int BatchId { get; set; }
    public DateTime ProcessedDate { get; set; }
    public string OutputFile { get; set; }
    public int? CollectionAccountId { get; set; }
    public int? PaymentAccountId { get; set; }
    public DateTime? CollectionValueDate { get; set; }
    public DateTime? PaymentValueDate { get; set; }
    public string SwiftUetr { get; set; }
    public int StatementLineId { get; set; }
    public string FileId { get; set; }
    public string FileName { get; set; }
    public DateTime FileCreationDate { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string OriginatorId { get; set; }
    public DateTime OriginatorDate { get; set; }
    public GroupHeaderStatus GroupStatus { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public string FundsType { get; set; }
    public string CkReference { get; set; }
    public string CustomerReferenceNumber { get; set; }
    public string CustomerAccountNumber { get; set; }
    public string BankReferenceNumber { get; set; }
    public int TypeCode { get; set; }
    public string TypeCodeDescription { get; set; }
    public string TypeCodeGroup { get; set; }
    public string ReferenceText { get; set; }
    public string PaymentReference { get; set; }
    public bool Duplication { get; set; }
    public int OutputTransferBatchId { get; set; }
    public string OutputTransferBatchFile { get; set; }
}

[DataContract]
public enum GroupHeaderStatus
{
    [EnumMember(Value = "Update")]
    Update = 1,

    [EnumMember(Value = "Deletion")]
    Deletion = 2,

    [EnumMember(Value = "Correction")]
    Correction = 3,

    [EnumMember(Value = "TestOnly")]
    TestOnly = 4
}