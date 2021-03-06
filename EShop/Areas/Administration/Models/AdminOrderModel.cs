﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Entities;
using EShop.Domain.Identity;

namespace EShop.Areas.Administration.Models
{
    public class AdminOrderModel
    {
        [Display(Name = "Номер заказа")]
        public int OrderId { get; set; }

        [Display(Name = "Корзина заказа")]
        public List<CartLine> OrderCart { get; set; }

        [Display(Name = "Пользователь")]
        public string User { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Адрес доставки")]
        public Shipment Shipment { get; set; }

        [Display(Name = "Общая стоимость")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Email")]
        public static SelectList List { get; set; }

        [Display(Name = "Статус заказа")]
        public int OrderStatus { get; set; }
    }
}