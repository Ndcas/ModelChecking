using System.ComponentModel.DataAnnotations;

namespace FinalLabWeb.Models;

public partial class Staff
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200)]
    [RegularExpression(@"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$")]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(30)]
    [RegularExpression(@"^(0|\+84)(3(2[5-9]|[3-9][2-9])|5(2[238]|59|6[2-9]|8[2-9]|9[2389])|7(0[2-8]|[6-9][2-9])|8([1-5][2-9]|6[259]|7[6-9]|[89][689])|9([0-4][1-9]|[6-8][1-9]|9[3-7]))\d{6}$")]
    public string Phone { get; set; } = null!;

    [Required]
    public DateOnly StartingDate { get; set; }

    [Required]
    [StringLength(100)]
    public string Photo { get; set; } = null!;
}
