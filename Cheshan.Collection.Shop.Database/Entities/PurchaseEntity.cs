namespace Cheshan.Collection.Shop.Database.Entities
{
    public class PurchaseEntity
    {
        /// <summary>
        /// Идентификатор для базы данных
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа (может быть дублирован, но зато читаемый)
        /// </summary>
        public string PurchaseId { get; set; }

        /// <summary>
        /// Имя заказчика
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия заказчика
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Email заказчика
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Телефон заказчика
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Тип доставки
        /// </summary>
        public string DeliveryType { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        public string DeliveryAdress { get; set; }

        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public string? CDEKOrderNumber { get; set; }

        /// <summary>
        /// Тип оплаты
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// Промокод
        /// </summary>
        public string? Promocode { get; set; }

        /// <summary>
        /// Цена для первого ИП
        /// </summary>
        public double PriceForSP1 { get; set; }

        /// <summary>
        /// Цена для второго ИП
        /// </summary>
        public double PriceForSP2 { get; set; }

        /// <summary>
        /// Оплачен ли заказ?
        /// </summary>
        public bool IsComplited { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Список ссылок для оплаты
        /// </summary>
        public virtual ICollection<PaymentLinkEntity> PaymentLinksWithPurchase { get; set; }

        /// <summary>
        /// Список заказанных продуктов
        /// </summary>
        public virtual ICollection<PurchasedProductEntity> PurchasedProducts { get; set; }

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            hash ^= Id.GetHashCode();
            hash ^= UserId.GetHashCode();
            return Math.Abs(hash);
        }
    }
}
