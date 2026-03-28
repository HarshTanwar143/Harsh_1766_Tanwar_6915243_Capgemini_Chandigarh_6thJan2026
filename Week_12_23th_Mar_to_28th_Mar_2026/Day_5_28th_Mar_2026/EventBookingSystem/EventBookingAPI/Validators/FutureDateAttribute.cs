using System.ComponentModel.DataAnnotations;

namespace EventBookingAPI.Validators;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime date)
            return date > DateTime.UtcNow;
        return false;
    }
}
