using Itmo.ObjectOrientedProgramming.Lab5.Application.DTOs;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.ViewModels;

public class TransactionViewModel
{
    public Guid Id { get; set; }

    public string? Date { get; set; }

    public string? Type { get; set; }

    public string? AmountWithCurrency { get; set; }

    public static TransactionViewModel FromDTO(TransactionDTO dto)
    {
        return new TransactionViewModel
        {
            Id = dto.Id,
            Date = dto.Date.ToString(),
            Type = dto.Type.ToString(),
            AmountWithCurrency = $"{dto.UsedMoney?.Amount}' '{dto.UsedMoney?.Currency}",
        };
    }

    public override string ToString()
    {
        return $"Id: {Id}, Date: {Date}, Type: {Type}, Amount: {AmountWithCurrency}";
    }
}