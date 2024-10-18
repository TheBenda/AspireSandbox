﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Domain.Entities;

public class OrderDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
