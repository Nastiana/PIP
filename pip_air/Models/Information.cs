//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pip_air.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Information
    {
        public int Id { get; set; }
        [Display(Name = "Номер рейсы")]
        public int Num_flight { get; set; }
        [Display(Name = "Город вылета")]
        public string Departure_place { get; set; }
        [Display(Name = "Город прибытия")]
        public string Arrival_place { get; set; }
        public System.DateTime Time_departure { get; set; }
        public System.DateTime Time_arrival { get; set; }
    
        public virtual Flight Flight { get; set; }
    }
}
