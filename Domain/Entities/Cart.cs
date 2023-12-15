using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Cart
{
    public int Id { get; set; }
    public int User_Id {get;set;}
    public User UserNavigation {get;set;}

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
